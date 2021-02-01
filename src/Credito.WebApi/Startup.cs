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
using Microsoft.OpenApi.Models;
using Credito.WebApi.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Credito.WebApi.ModelProviders;
using Credito.WebApi.Filters;
using Microsoft.AspNetCore.Mvc.Versioning;

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

            services
                .AddMvcCore(
                    options =>
                    {
                        options.Filters.Add<LoggingActionFilter>();
                    })
                .AddDataAnnotations()
                .AddApiExplorer()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(AppDependencyInjection).Assembly));

            services.AddApiVersioning(
                config =>
                {
                    config.DefaultApiVersion = new ApiVersion(1, 0);
                    config.AssumeDefaultVersionWhenUnspecified = true;
                    config.ReportApiVersions = true;
                    config.ApiVersionReader = new HeaderApiVersionReader("api-version");
                });

            services.Configure<ApiBehaviorOptions>(
                options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IApplicationModelProvider, ProducesResponseTypeDefaultErrorsModelProvider>());

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

            app.UseSerilogRequestLogging(
                loggingOptions =>
                {
                    loggingOptions.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                    {
                        diagnosticContext.Set("TraceIdentifier", httpContext.TraceIdentifier);
                    };
                });

            app.UseMiddleware<SerilogAddTraceIdentifierMiddleware>()
               .UseExceptionHandlerMiddleware()
               .UseRouting()
               .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
