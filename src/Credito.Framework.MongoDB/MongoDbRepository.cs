using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Credito.Domain.Common;
using Credito.Domain.ContratoDeEmprestimo;
using Credito.Framework.MongoDB.Example;
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

        public T Load<T>(Guid id) where T : AggregateRoot =>
            GetCollection<T>().AsQueryable()
                              .Where(x => x.Id == id)
                              .FirstOrDefault();

        public IList<T> Find<T>(Expression<Func<T, bool>> filter,
                                int skip = 0,
                                int take = 10) =>
            GetCollection<T>().AsQueryable()
                              .Where(filter)
                              .Skip(skip)
                              .Take(take)
                              .ToList();

        public IList<T> Get<T>(int skip = 0, int take = 10) =>
            GetCollection<T>().AsQueryable()
                              .Skip(skip)
                              .Take(take)
                              .ToList();

        public void Insert<T>(T aggregate) =>
            GetCollection<T>().InsertOne(aggregate);

        public void Update<T>(T aggregate) where T : AggregateRoot =>
            GetCollection<T>().ReplaceOne(x => x.Id == aggregate.Id, aggregate);

        public void Remove<T>(Guid id) where T : AggregateRoot =>
            GetCollection<T>().DeleteOne(x => x.Id == id);
    }
}