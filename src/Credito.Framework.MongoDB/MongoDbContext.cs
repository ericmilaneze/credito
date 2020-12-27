using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Bson.Serialization;
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

            BsonSerializer.RegisterSerializer(typeof(Idade), new IdadeSerializer());
        }

        public void Insert(Aggregate aggregate)
        {
            var collection = Database.GetCollection<Aggregate>("aggregates");
            collection.InsertOne(aggregate);
        }

        public void Update(Aggregate aggregate)
        {
            var collection = Database.GetCollection<Aggregate>("aggregates");
            collection.ReplaceOne(x => x.Id == aggregate.Id, aggregate);
        }

        public Aggregate Load(Guid id)
        {
            var collection = Database.GetCollection<Aggregate>("aggregates");
            return collection.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
        }

        public Aggregate Remove(Guid id)
        {
            var collection = Database.GetCollection<Aggregate>("aggregates");
            collection.DeleteOne(x => x.Id == id);
            return collection.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
        }

        public IList<Aggregate> Find(Expression<Func<Aggregate, bool>> filter)
        {
            var collection = Database.GetCollection<Aggregate>("aggregates");
            return collection.Find(filter).ToList();
        }
    }
}