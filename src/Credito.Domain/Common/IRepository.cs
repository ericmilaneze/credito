using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Credito.Domain.Common
{
    public interface IRepository<T> where T : AggregateRoot
    {
        Task<IList<T>> FindAsync(Expression<Func<T, bool>> filter, int skip = 0, int take = 10, CancellationToken cancellationToken = default);
        Task<IList<T>> GetAsync(int skip = 0, int take = 10, CancellationToken cancellationToken = default);
        Task InsertAsync(T aggregate, CancellationToken cancellationToken = default);
        Task<T> LoadAsync(Guid id, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        Task SaveAsync(T aggregate, CancellationToken cancellationToken = default);
        Task UpdateAsync(T aggregate, CancellationToken cancellationToken = default);
    }
}