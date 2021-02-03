using System;
using Credito.WebApi.ModelProviders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Credito.WebApi.StartupExtensions
{
    public static class DefaultErrorResponsesConfiguration
    {
        public static IServiceCollection AddDefaultErrorResponsesConfiguration(this IServiceCollection services)
        {
            services.Configure(GetConfigureOptions())
                    .TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ProducesResponseTypeDefaultErrorsModelProvider>());

            return services;

            Action<ApiBehaviorOptions> GetConfigureOptions() =>
                options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                };
        }
    }
}