using System;
using System.Reflection;
using Credito.Domain.Common.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Credito.Framework.MongoDB.Serializers
{
    public class ValueFromDecimalSerializer<T> : SerializerBase<T> where T : ValueFromDecimal
    {
        public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(decimal));
            var data = serializer.Deserialize(context, args);
            return (T)Activator.CreateInstance(
                typeof(T),
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new object[] { (decimal)data },
                null);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(decimal));
            serializer.Serialize(context, ValueFromDecimal.ToDecimalNullable(value));
        }
    }
}