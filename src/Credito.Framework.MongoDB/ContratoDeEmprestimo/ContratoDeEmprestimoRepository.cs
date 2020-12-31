using Credito.Domain.ContratoDeEmprestimo;

namespace Credito.Framework.MongoDB.ContratoDeEmprestimo
{
    public class ContratoDeEmprestimoRepository : MongoDbRepository<ContratoDeEmprestimoAggregate>,
                                                  IContratoDeEmprestimoRepository
    {
        public ContratoDeEmprestimoRepository(IMongoDbContext context) : base(context)
        {
        }
    }
}