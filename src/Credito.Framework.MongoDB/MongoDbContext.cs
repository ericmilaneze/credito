using Credito.Framework.MongoDB.Registry;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Credito.Framework.MongoDB
{
    public sealed class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;

        static MongoDbContext() =>
            MongoDbSettings.RegiterSettings();

        public MongoDbContext(string connectionString, string dbName)
        {
            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection<T>() =>
            _database.GetCollection<T>(CollectionNamesRegistry.GetCollectionName<T>());

        public IMongoCollection<T> GetCollection<T>(string collectionName) =>
            _database.GetCollection<T>(collectionName);

        public IMongoCollection<BsonDocument> GetCollection(string collectionName) =>
            _database.GetCollection<BsonDocument>(collectionName);
    }
}