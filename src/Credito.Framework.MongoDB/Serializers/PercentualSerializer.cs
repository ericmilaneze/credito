using System;
using System.Reflection;
using Credito.Domain.Common.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Credito.Framework.MongoDB.Serializers
{
    public class PercentualSerializer : SerializerBase<Percentual>
    {
        public override Percentual Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(decimal));
            var data = serializer.Deserialize(context, args);
            return (Percentual)Activator.CreateInstance(
                typeof(Percentual),
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new object[] { (decimal)data },
                null);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Percentual value)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(decimal));
            serializer.Serialize(context, value.ToDecimal());
        }
    }
}