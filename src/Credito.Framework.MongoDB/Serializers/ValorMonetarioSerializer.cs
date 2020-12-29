using System;
using System.Reflection;
using Credito.Domain.Common.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Credito.Framework.MongoDB.Serializers
{
    public class ValorMonetarioSerializer : SerializerBase<ValorMonetario>
    {
        public override ValorMonetario Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(decimal));
            var data = serializer.Deserialize(context, args);
            return (ValorMonetario)Activator.CreateInstance(
                typeof(ValorMonetario),
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new object[] { (decimal)data },
                null);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ValorMonetario value)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(decimal));
            serializer.Serialize(context, value.ToDecimal());
        }
    }
}