using System.Linq;
using System.Net;
using AutoMapper;
using Credito.Application;
using Credito.Application.Common.Behaviors;
using Credito.Domain.Common;
using Credito.Domain.ContratoDeEmprestimo;
using Credito.Framework.MongoDB;
using Credito.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace Credito.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            services.AddAutoMapper(this.GetType().Assembly);

            services.AddSingleton<IMongoDbContext>(new MongoDbContext("mongodb://localhost", "test"))
                    .AddScoped<IDbRepository<ContratoDeEmprestimoAggregate>, MongoDbRepository<ContratoDeEmprestimoAggregate>>()
                    .AddScoped<IContratoDeEmprestimoRepository, ContratoDeEmprestimoRepository>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                    .AddMediatR(typeof(AssemblyInfo).Assembly);

            services.AddMvcCore()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(AssemblyInfo).Assembly))
                    .AddDataAnnotations()
                    .AddApiExplorer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionFeature.Error is ValidationException validationException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsJsonAsync(
                        new 
                        {
                            message = validationException.Message,
                            fields = validationException.Errors
                                .GroupBy(x => x.PropertyName)
                                .Select(x => new 
                                            { 
                                                Name = x.Key, 
                                                ErrorMessage = x.Select(v => v.ErrorMessage) 
                                            })
                        });
                }
            }));

            app.UseSerilogRequestLogging()
               .UseHttpsRedirection()
               .UseRouting()
               .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
