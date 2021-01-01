using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Credito.Domain.Common;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Credito.Framework.MongoDB
{
    public sealed class MongoDbRepository<TAggregate> : IDbRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        private readonly IMongoDbContext _mongoDbContext;

        public MongoDbRepository(IMongoDbContext mongoDbContext) =>
            _mongoDbContext = mongoDbContext;

        public async Task<TAggregate> LoadAsync(Guid id,
                                       CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<TAggregate>().AsQueryable()
                                                             .Where(x => x.Id == id)
                                                             .FirstOrDefaultAsync(cancellationToken);

        public async Task<IList<TAggregate>> FindAsync(Expression<Func<TAggregate, bool>> filter,
                                                       int skip = 0,
                                                       int take = 10,
                                                       CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<TAggregate>().AsQueryable()
                                                             .Where(filter)
                                                             .Skip(skip)
                                                             .Take(take)
                                                             .ToListAsync(cancellationToken);

        public async Task<IList<TAggregate>> GetAsync(int skip = 0,
                                                      int take = 10,
                                                      CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<TAggregate>().AsQueryable()
                                                             .Skip(skip)
                                                             .Take(take)
                                                             .ToListAsync(cancellationToken);

        public async Task InsertAsync(TAggregate aggregate,
                                      CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<TAggregate>().InsertOneAsync(aggregate, 
                                                                             new InsertOneOptions(),
                                                                             cancellationToken);

        public async Task UpdateAsync(TAggregate aggregate,
                                      CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<TAggregate>().ReplaceOneAsync(x => x.Id == aggregate.Id, 
                                                                              aggregate,
                                                                              new ReplaceOptions(),
                                                                              cancellationToken);
                                                            
        public async Task SaveAsync(TAggregate aggregate,
                                    CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<TAggregate>().ReplaceOneAsync(x => x.Id == aggregate.Id, 
                                                                              aggregate, 
                                                                              new ReplaceOptions() { IsUpsert = true },
                                                                              cancellationToken);

        public async Task RemoveAsync(Guid id,
                                      CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<TAggregate>().DeleteOneAsync(x => x.Id == id,
                                                                             cancellationToken);
    }
}