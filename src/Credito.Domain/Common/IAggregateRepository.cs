using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Credito.Domain.Common
{
    public interface IAggregateRepository<TAggregate> where TAggregate : AggregateRoot
    {
        Task<IList<TAggregate>> FindAsync(Expression<Func<TAggregate, bool>> filter,
                                          int skip = 0,
                                          int take = 10,
                                          CancellationToken cancellationToken = default);
        Task<IList<TAggregate>> GetAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default);
        Task SaveAsync(TAggregate aggregate, CancellationToken cancellationToken = default);
        Task<TAggregate> LoadAsync(Guid id, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
    }
}