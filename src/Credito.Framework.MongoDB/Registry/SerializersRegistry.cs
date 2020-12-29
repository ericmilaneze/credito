using Credito.Framework.MongoDB.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Credito.Framework.MongoDB.Registry
{
    public class SerializersRegistry
    {
        internal static void RegisterSerializers()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));

            BsonSerializer.RegisterSerializer(new IdadeExampleSerializer());
            BsonSerializer.RegisterSerializer(new PercentualPositivoSerializer());
            BsonSerializer.RegisterSerializer(new PercentualSerializer());
            BsonSerializer.RegisterSerializer(new PrazoSerializer());
            BsonSerializer.RegisterSerializer(new ValorMonetarioPositivoSerializer());
            BsonSerializer.RegisterSerializer(new ValorMonetarioSerializer());
            BsonSerializer.RegisterSerializer(new NumeroParcelaSerializer());
        }
    }
}