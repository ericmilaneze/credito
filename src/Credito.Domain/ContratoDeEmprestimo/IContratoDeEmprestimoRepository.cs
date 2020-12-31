using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Credito.Domain.ContratoDeEmprestimo
{
    public interface IContratoDeEmprestimoRepository
    {
        Task<IList<ContratoDeEmprestimoAggregate>> FindAsync(Expression<Func<ContratoDeEmprestimoAggregate, bool>> filter, int skip = 0, int take = 10, CancellationToken cancellationToken = default);
        Task<IList<ContratoDeEmprestimoAggregate>> GetAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default);
        Task<ContratoDeEmprestimoAggregate> LoadAsync(Guid id, CancellationToken cancellationToken = default);
        Task SaveAsync(ContratoDeEmprestimoAggregate aggregate, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
    }
}