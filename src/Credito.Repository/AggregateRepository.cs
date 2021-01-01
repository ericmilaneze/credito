using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Credito.Domain.Common;

namespace Credito.Repository
{
    public abstract class AggregateRepository<TAggregate> : IAggregateRepository<TAggregate> 
        where TAggregate : AggregateRoot
    {
        protected readonly IDbRepository<TAggregate> _repository;

        public AggregateRepository(IDbRepository<TAggregate> repository)
        {
            _repository = repository;
        }

        public async Task<IList<TAggregate>> FindAsync(Expression<Func<TAggregate, bool>> filter,
                                                       int skip = 0,
                                                       int take = 10,
                                                       CancellationToken cancellationToken = default) =>
            await _repository.FindAsync(filter, skip, take, cancellationToken);

        public async Task<IList<TAggregate>> GetAsync(int skip = 0,
                                                      int take = 10,
                                                      CancellationToken cancellationToken = default) =>
            await _repository.GetAsync(skip, take, cancellationToken);

        public async Task<TAggregate> LoadAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _repository.LoadAsync(id, cancellationToken);

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _repository.RemoveAsync(id, cancellationToken);

        public async Task SaveAsync(TAggregate aggregate, CancellationToken cancellationToken = default) =>
            await _repository.SaveAsync(aggregate, cancellationToken);
    }
}