using System;
using Credito.Application;
using Credito.WebApi.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.AspNetCore;
using Microsoft.OpenApi.Models;
using Credito.WebApi.Misc;

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

            AppDependencyInjection.ConfigureServices(services);

            services.AddMvcCore()
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(AppDependencyInjection).Assembly))
                    .AddDataAnnotations()
                    .AddApiExplorer();

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc(Globals.SWAGGER_V1, new OpenApiInfo { Title = Globals.SWAGGER_API_NAME, Version = Globals.SWAGGER_V1_NAME });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger(
                    c =>
                    {
                        c.RouteTemplate = $"/{Globals.SWAGGER_BASE_PATH}/{{documentName}}/swagger.json";
                    });
                app.UseSwaggerUI(
                    c =>
                    {
                        c.RoutePrefix = $"{Globals.SWAGGER_BASE_PATH}";
                        c.SwaggerEndpoint($"/{Globals.SWAGGER_BASE_PATH}/{Globals.SWAGGER_V1}/swagger.json", $"{Globals.SWAGGER_API_NAME} - {Globals.SWAGGER_V1_NAME}");
                    });
            }

            app.UseSerilogRequestLogging(GetSerilogConfigureOptions())
               .UseMiddleware<SerilogAddTraceIdentifierMiddleware>()
               .UseExceptionHandlerMiddleware()
               .UseRouting()
               .UseEndpoints(endpoints => endpoints.MapControllers());

            Action<RequestLoggingOptions> GetSerilogConfigureOptions() =>
                loggingOptions =>
                {
                    loggingOptions.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                    {
                        diagnosticContext.Set("TraceIdentifier", httpContext.TraceIdentifier);
                    };
                };
        }
    }
}
