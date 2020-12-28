using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Credito.Framework.MongoDB
{
    public class MongoDbContext
    {
        private readonly MongoClient mongoClient;

        private IMongoDatabase Database => mongoClient.GetDatabase("test");

        public MongoDbContext(string connectionString)
        {
            this.mongoClient = new MongoClient(connectionString);

            ConventionRegistry.Register(
                "Default", 
                new ConventionPack
                {
                    new CamelCaseElementNameConvention(),
                    new IgnoreIfNullConvention(true),
                    new EnumRepresentationConvention(BsonType.String)
                },
                _ => true);
            BsonSerializer.RegisterSerializer(new IdadeSerializer());
            BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));
        }

        public IMongoCollection<Aggregate> GetCollection() =>
            Database.GetCollection<Aggregate>("aggregates");

        public Aggregate Load(Guid id) =>
            GetCollection().AsQueryable()
                           .Where(x => x.Id == id)
                           .FirstOrDefault();

        public IList<Aggregate> Find(Expression<Func<Aggregate, bool>> filter,
                                     int skip = 0,
                                     int take = 10) =>
            GetCollection().AsQueryable()
                           .Where(filter)
                           .Skip(skip)
                           .Take(take)
                           .ToList();

        public IList<Aggregate> Get(int skip = 0, int take = 10) =>
            GetCollection().AsQueryable()
                           .Skip(skip)
                           .Take(take)
                           .ToList();

        public void Insert(Aggregate aggregate) =>
            GetCollection().InsertOne(aggregate);

        public void Update(Aggregate aggregate) =>
            GetCollection().ReplaceOne(x => x.Id == aggregate.Id, aggregate);

        public void Remove(Guid id) =>
            GetCollection().DeleteOne(x => x.Id == id);
    }
}