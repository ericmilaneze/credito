using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Credito.Domain.Common;
using MongoDB.Driver;

namespace Credito.Framework.MongoDB
{
    public interface IMongoDbRepository
    {
        Task<IList<T>> FindAsync<T>(Expression<Func<T, bool>> filter, int skip = 0, int take = 10, CancellationToken cancellationToken = default);
        Task<IList<T>> GetAsync<T>(int skip = 0, int take = 10, CancellationToken cancellationToken = default);
        IMongoCollection<T> GetCollection<T>();
        Task InsertAsync<T>(T aggregate, CancellationToken cancellationToken = default);
        Task<T> LoadAsync<T>(Guid id, CancellationToken cancellationToken = default) where T : AggregateRoot;
        Task RemoveAsync<T>(Guid id, CancellationToken cancellationToken = default) where T : AggregateRoot;
        Task UpdateAsync<T>(T aggregate, CancellationToken cancellationToken = default) where T : AggregateRoot;
    }
}