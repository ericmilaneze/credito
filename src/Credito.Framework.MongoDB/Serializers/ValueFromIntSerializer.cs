using System;
using System.Reflection;
using Credito.Domain.Common.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Credito.Framework.MongoDB.Serializers
{
    public class ValueFromIntSerializer<T> : SerializerBase<T> where T : ValueFromInt
    {
        public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(int));
            var data = serializer.Deserialize(context, args);
            return (T)Activator.CreateInstance(
                typeof(T),
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new object[] { (int)data },
                null);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(int));
            serializer.Serialize(context, ValueFromInt.ToIntNullable(value));
        }
    }
}