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

namespace Credito.WebApi
{
    public class Startup
    {
        private readonly string SWAGGER_API_NAME = "API de Crédito";
        private readonly string SWAGGER_BASE_PATH = "api/swagger";
        private readonly string SWAGGER_V1 = "v1";
        private readonly string SWAGGER_V1_NAME = "Versão 1";

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
                    c.SwaggerDoc(SWAGGER_V1, new OpenApiInfo { Title = SWAGGER_API_NAME, Version = SWAGGER_V1_NAME });
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
                        c.RouteTemplate = $"/{SWAGGER_BASE_PATH}/{{documentName}}/swagger.json";
                    });
                app.UseSwaggerUI(
                    c =>
                    {
                        c.RoutePrefix = $"{SWAGGER_BASE_PATH}";
                        c.SwaggerEndpoint($"/{SWAGGER_BASE_PATH}/{SWAGGER_V1}/swagger.json", $"{SWAGGER_API_NAME} - {SWAGGER_V1_NAME}");
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
