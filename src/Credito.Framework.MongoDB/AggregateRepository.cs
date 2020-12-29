using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Credito.Domain.Common;

namespace Credito.Framework.MongoDB
{
    public abstract class AggregateRepository<T> where T : AggregateRoot
    {
        protected readonly IMongoDbRepository _mongoDbRepository;

        public AggregateRepository(IMongoDbRepository mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;
        }

        public async Task<T> LoadAsync(Guid id,
                                       CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbRepository.LoadAsync<T>(id, cancellationToken);

        public async Task<IList<T>> FindAsync(Expression<Func<T, bool>> filter,
                                              int skip = 0,
                                              int take = 10,
                                              CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbRepository.FindAsync(filter, skip, take, cancellationToken);

        public async Task<IList<T>> GetAsync(int skip = 0,
                                             int take = 10,
                                             CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbRepository.GetAsync<T>(skip, take, cancellationToken);

        public async Task InsertAsync(T aggregate,
                                      CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbRepository.InsertAsync(aggregate, cancellationToken);

        public async Task UpdateAsync(T aggregate,
                                      CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbRepository.UpdateAsync(aggregate, cancellationToken);

        public async Task RemoveAsync(Guid id,
                                      CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbRepository.RemoveAsync<T>(id, cancellationToken);
    }
}