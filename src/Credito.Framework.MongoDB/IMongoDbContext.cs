using MongoDB.Bson;
using MongoDB.Driver;

namespace Credito.Framework.MongoDB
{
    public interface IMongoDbContext
    {
        IMongoCollection<T> GetCollection<T>();
        IMongoCollection<BsonDocument> GetCollection(string collectionName);
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}