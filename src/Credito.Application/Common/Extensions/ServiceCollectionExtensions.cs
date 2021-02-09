using Microsoft.Extensions.DependencyInjection;

using FluentValidation;
using System.Reflection;

namespace Credito.Application.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFluentValidation(this IServiceCollection services, Assembly assembly)
        {
            AssemblyScanner.FindValidatorsInAssembly(typeof(AppDependencyInjection).Assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            return services;
        }
    }
}