using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Credito.Domain.Common;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Credito.Framework.MongoDB
{
    public class MongoDbRepository
    {
        private readonly IMongoDatabase _database;

        public MongoDbRepository(string connectionString, string dbName)
        {
            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase(dbName);
            
            RegisterConventions();
            RegisterSerializers();
        }

        private static void RegisterSerializers()
        {
            BsonSerializer.RegisterSerializer(new IdadeSerializer());
            BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));
        }

        private static void RegisterConventions()
        {
            ConventionRegistry.Register("Default",
                                        new ConventionPack
                                        {
                                            new CamelCaseElementNameConvention(),
                                            new IgnoreIfNullConvention(true),
                                            new EnumRepresentationConvention(BsonType.String)
                                        },
                                        _ => true);
        }

        public IMongoCollection<T> GetCollection<T>() =>
            _database.GetCollection<T>("aggregates");

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