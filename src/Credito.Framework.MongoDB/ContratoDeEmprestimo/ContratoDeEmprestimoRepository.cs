using Credito.Domain.ContratoDeEmprestimo;

namespace Credito.Framework.MongoDB.ContratoDeEmprestimo
{
    public class ContratoDeEmprestimoRepository : AggregateRepository<ContratoDeEmprestimoAggregate>, IContratoDeEmprestimoRepository
    {
        public ContratoDeEmprestimoRepository(IMongoDbRepository mongoDbRepository) : base(mongoDbRepository)
        {
        }
    }
}