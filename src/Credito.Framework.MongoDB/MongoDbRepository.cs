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
    public abstract class MongoDbRepository<T> : IRepository<T> where T : AggregateRoot
    {
        private readonly IMongoDbContext _mongoDbContext;

        public MongoDbRepository(IMongoDbContext mongoDbContext) =>
            _mongoDbContext = mongoDbContext;

        public async Task<T> LoadAsync(Guid id,
                                       CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<T>().AsQueryable()
                                                    .Where(x => x.Id == id)
                                                    .FirstOrDefaultAsync(cancellationToken);

        public async Task<IList<T>> FindAsync(Expression<Func<T, bool>> filter,
                                              int skip = 0,
                                              int take = 10,
                                              CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<T>().AsQueryable()
                                                    .Where(filter)
                                                    .Skip(skip)
                                                    .Take(take)
                                                    .ToListAsync(cancellationToken);

        public async Task<IList<T>> GetAsync(int skip = 0,
                                             int take = 10,
                                             CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<T>().AsQueryable()
                                                    .Skip(skip)
                                                    .Take(take)
                                                    .ToListAsync(cancellationToken);

        public async Task InsertAsync(T aggregate,
                                      CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<T>().InsertOneAsync(aggregate, 
                                                                    new InsertOneOptions(),
                                                                    cancellationToken);

        public async Task UpdateAsync(T aggregate,
                                      CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<T>().ReplaceOneAsync(x => x.Id == aggregate.Id, 
                                                                     aggregate,
                                                                     new ReplaceOptions(),
                                                                     cancellationToken);
                                                            
        public async Task SaveAsync(T aggregate,
                                    CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<T>().ReplaceOneAsync(x => x.Id == aggregate.Id, 
                                                                     aggregate, 
                                                                     new ReplaceOptions() { IsUpsert = true },
                                                                     cancellationToken);

        public async Task RemoveAsync(Guid id,
                                      CancellationToken cancellationToken = default(CancellationToken)) =>
            await _mongoDbContext.GetCollection<T>().DeleteOneAsync(x => x.Id == id,
                                                                    cancellationToken);
    }
}