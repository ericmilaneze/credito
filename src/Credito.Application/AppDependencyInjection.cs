using AutoMapper;
using Credito.Application.Common.Behaviors;
using Credito.Application.Common.Extensions;
using Credito.Application.Common.Settings;
using Credito.Application.v1_0.ContratoDeEmprestimo.QueryHandlers.QueryImplementations;
using Credito.Application.v1_0.ContratoDeEmprestimo.QueryHandlers.QueryImplementations.Interfaces;
using Credito.Domain.Common;
using Credito.Domain.ContratoDeEmprestimo;
using Credito.Framework.MongoDB;
using Credito.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Credito.Application
{
    public static class AppDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
                                                        IConfiguration configuration) =>
            services
                .AddAutoMapper(typeof(MappingProfile).Assembly)
                .AddSingleton((IMongoDbContext)new MongoDbContext(configuration.GetSection("CreditoDatabase")
                                                                               .Get<CreditoDatabaseSettings>()
                                                                               .ConnectionString,
                                                                  configuration.GetSection("CreditoDatabase")
                                                                               .Get<CreditoDatabaseSettings>()
                                                                               .DatabaseName))
                .AddSingleton<IDbRepository<ContratoDeEmprestimoAggregate>, MongoDbRepository<ContratoDeEmprestimoAggregate>>()
                .AddSingleton<IContratoDeEmprestimoRepository, ContratoDeEmprestimoRepository>()
                .AddSingleton<IObterContratos, ContratoQueries>()
                .AddSingleton<IObterContratoPorId, ContratoQueries>()
                .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddMediatR(typeof(AppDependencyInjection).Assembly)
                .AddFluentValidation(typeof(AppDependencyInjection).Assembly);
    }
}