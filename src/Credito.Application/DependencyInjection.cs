using AutoMapper;
using Credito.Application.Common.Behaviors;
using Credito.Domain.Common;
using Credito.Domain.ContratoDeEmprestimo;
using Credito.Framework.MongoDB;
using Credito.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Credito.Application
{
    public class DependencyInjection
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            services.AddSingleton<IMongoDbContext>(new MongoDbContext("mongodb://localhost", "test"));

            services.AddScoped<IDbRepository<ContratoDeEmprestimoAggregate>, MongoDbRepository<ContratoDeEmprestimoAggregate>>()
                    .AddScoped<IContratoDeEmprestimoRepository, ContratoDeEmprestimoRepository>();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                    .AddMediatR(typeof(DependencyInjection).Assembly);
        }
    }
}