using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Credito.Framework.MongoDB
{
    public class MongoTrial
    {
        private readonly string connectionString;

        public MongoTrial(string connectionString)
        {
            this.connectionString = connectionString;

            BsonSerializer.RegisterSerializer(typeof(Idade), new IdadeSerializer());
        }

        public void Insert(Aggregate aggregate)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var collection = db.GetCollection<Aggregate>("aggregates");
            collection.InsertOne(aggregate);
        }

        public Aggregate Load(string id)
        {
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            
            var collection = db.GetCollection<Aggregate>("aggregates");
            return collection.AsQueryable().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}