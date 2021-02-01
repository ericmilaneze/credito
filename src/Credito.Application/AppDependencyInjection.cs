using AutoMapper;
using Credito.Application.Common.Behaviors;
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
    public class AppDependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();

            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            var creditoDatabaseSettings = configuration.GetSection("CreditoDatabase").Get<CreditoDatabaseSettings>();
            services.AddSingleton<IMongoDbContext>(new MongoDbContext(creditoDatabaseSettings.ConnectionString,
                                                                      creditoDatabaseSettings.DatabaseName));

            services.AddSingleton<IDbRepository<ContratoDeEmprestimoAggregate>, MongoDbRepository<ContratoDeEmprestimoAggregate>>()
                    .AddSingleton<IContratoDeEmprestimoRepository, ContratoDeEmprestimoRepository>()
                    .AddSingleton<IObterContratos, ContratoQueries>()
                    .AddSingleton<IObterContratoPorId, ContratoQueries>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                    .AddMediatR(typeof(AppDependencyInjection).Assembly);
        }
    }
}