using Credito.Domain.Common.ValueObjects;
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
            BsonSerializer.RegisterSerializer(new ValueFromDecimalSerializer<PercentualPositivo>());
            BsonSerializer.RegisterSerializer(new ValueFromDecimalSerializer<Percentual>());
            BsonSerializer.RegisterSerializer(new ValueFromDecimalSerializer<ValorMonetarioPositivo>());
            BsonSerializer.RegisterSerializer(new ValueFromDecimalSerializer<ValorMonetario>());
            BsonSerializer.RegisterSerializer(new ValueFromIntSerializer<Prazo>());
            BsonSerializer.RegisterSerializer(new ValueFromIntSerializer<NumeroParcela>());
        }
    }
}