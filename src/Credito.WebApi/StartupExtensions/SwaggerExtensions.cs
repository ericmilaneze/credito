using System.Linq;
using Credito.WebApi.Misc;
using Credito.WebApi.SwaggerConfigurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Credito.WebApi.StartupExtensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services) =>
            services
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen(
                    options =>
                    {
                        options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                        options.OperationFilter<SwaggerDefaultValues>();
                    });

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app,
                                                                  IWebHostEnvironment env,
                                                                  IApiVersionDescriptionProvider provider) =>
            app
                .UseSwagger(
                    options =>
                    {
                        options.RouteTemplate = $"/{Globals.SWAGGER_BASE_PATH}/{{documentName}}/swagger.json";
                    })
                .UseSwaggerUI(
                    options =>
                    {
                        foreach (var description in provider.ApiVersionDescriptions)
                        {
                            options.RoutePrefix = $"{Globals.SWAGGER_BASE_PATH}";
                            options.SwaggerEndpoint($"/{Globals.SWAGGER_BASE_PATH}/{description.GroupName}/swagger.json", $"{Globals.SWAGGER_API_NAME} - {description.GroupName.ToUpperInvariant()}");
                        }

                        options.DisplayOperationId();
                        options.DisplayRequestDuration();
                    });
    }
}