using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Credito.Domain.Common;
using Credito.Framework.MongoDB.Registry;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Credito.Framework.MongoDB
{
    public class MongoDbRepository
    {
        private readonly IMongoDatabase _database;

        static MongoDbRepository()
        {
            ConventionsRegistry.RegisterConventions();
            SerializersRegistry.RegisterSerializers();
            ClassMapsRegistry.RegisterClassMaps();
        }

        public MongoDbRepository(string connectionString, string dbName)
        {
            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection<T>() =>
            _database.GetCollection<T>(CollectionNamesRegistry.GetCollectionName<T>());

        public async Task<T> LoadAsync<T>(Guid id,
                                          CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot =>
            await GetCollection<T>().AsQueryable()
                                    .Where(x => x.Id == id)
                                    .FirstOrDefaultAsync(cancellationToken);

        public async Task<IList<T>> FindAsync<T>(Expression<Func<T, bool>> filter,
                                                 int skip = 0,
                                                 int take = 10,
                                                 CancellationToken cancellationToken = default(CancellationToken)) =>
            await GetCollection<T>().AsQueryable()
                                    .Where(filter)
                                    .Skip(skip)
                                    .Take(take)
                                    .ToListAsync(cancellationToken);

        public async Task<IList<T>> GetAsync<T>(int skip = 0,
                                                int take = 10,
                                                CancellationToken cancellationToken = default(CancellationToken)) =>
            await GetCollection<T>().AsQueryable()
                                    .Skip(skip)
                                    .Take(take)
                                    .ToListAsync(cancellationToken);

        public async Task InsertAsync<T>(T aggregate,
                                         CancellationToken cancellationToken = default(CancellationToken)) =>
            await GetCollection<T>().InsertOneAsync(aggregate, new InsertOneOptions(), cancellationToken);

        public async Task UpdateAsync<T>(T aggregate,
                                         CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot =>
            await GetCollection<T>().ReplaceOneAsync(x => x.Id == aggregate.Id, aggregate, new ReplaceOptions(), cancellationToken);

        public async Task RemoveAsync<T>(Guid id,
                                         CancellationToken cancellationToken = default(CancellationToken)) where T : AggregateRoot =>
            await GetCollection<T>().DeleteOneAsync(x => x.Id == id, cancellationToken);
    }
}