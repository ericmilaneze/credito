using Credito.Application;
using Credito.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Credito.WebApi.Filters;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Credito.WebApi.StartupExtensions;

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
            JsonConvert.DefaultSettings = () => 
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

            services
                .AddApplication(Configuration)
                .AddMvcCore(
                    options =>
                    {
                        options.Filters.Add<LoggingActionFilter>();
                    })
                .AddDataAnnotations()
                .AddApiExplorer()
                .AddVersioning()
                .AddDefaultErrorResponsesConfiguration()
                .AddSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app
                .UseSwaggerConfiguration(env, provider)
                .UseSerilogRequestLogging(
                    loggingOptions =>
                    {
                        loggingOptions.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                        {
                            diagnosticContext.Set("TraceIdentifier", httpContext.TraceIdentifier);
                        };
                    })
                .UseMiddleware<SerilogAddTraceIdentifierMiddleware>()
                .UseExceptionHandlerMiddleware()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
