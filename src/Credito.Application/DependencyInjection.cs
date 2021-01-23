using AutoMapper;
using Credito.Application.Common.Behaviors;
using Credito.Application.Common.Settings;
using Credito.Domain.Common;
using Credito.Domain.ContratoDeEmprestimo;
using Credito.Framework.MongoDB;
using Credito.Repository;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Credito.Application
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();

            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            var creditoDatabaseSettings = configuration.GetSection("CreditoDatabase").Get<CreditoDatabaseSettings>();
            services.AddSingleton<IMongoDbContext>(new MongoDbContext(creditoDatabaseSettings.ConnectionString,
                                                                      creditoDatabaseSettings.DatabaseName));

            services.AddScoped<IDbRepository<ContratoDeEmprestimoAggregate>, MongoDbRepository<ContratoDeEmprestimoAggregate>>()
                    .AddScoped<IContratoDeEmprestimoRepository, ContratoDeEmprestimoRepository>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                    .AddMediatR(typeof(DependencyInjection).Assembly);
        }
    }
}