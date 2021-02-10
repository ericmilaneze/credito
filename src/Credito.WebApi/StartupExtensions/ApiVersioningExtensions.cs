using System;
using Credito.WebApi.Misc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Credito.WebApi.StartupExtensions
{
    public static class ApiVersioningExtensions
    {
        public static IServiceCollection AddVersioning(this IServiceCollection services) =>
            services.AddApiVersioning(GetApiVersioningSetupAction())
                    .AddVersionedApiExplorer(GetVersionedApiSetupAction());

        public static IServiceCollection AddVersioning(this IMvcCoreBuilder mvcBuilder) =>
            mvcBuilder.Services.AddVersioning();

        private static Action<ApiExplorerOptions> GetVersionedApiSetupAction() =>
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.ApiVersionParameterSource = new HeaderApiVersionReader(Globals.API_VERSION_HEADER);
                options.AssumeDefaultVersionWhenUnspecified = true;
            };

        private static Action<ApiVersioningOptions> GetApiVersioningSetupAction() =>
            config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new HeaderApiVersionReader(Globals.API_VERSION_HEADER);
            };
    }
}