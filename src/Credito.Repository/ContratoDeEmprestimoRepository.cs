using Credito.Domain.Common;
using Credito.Domain.ContratoDeEmprestimo;

namespace Credito.Repository
{
    public class ContratoDeEmprestimoRepository : AggregateRepository<ContratoDeEmprestimoAggregate>,
                                                  IContratoDeEmprestimoRepository
    {
        public ContratoDeEmprestimoRepository(IDbRepository<ContratoDeEmprestimoAggregate> repository)
            : base(repository)
        {
        }
    }
}