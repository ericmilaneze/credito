using System;
using System.Reflection;
using Credito.Domain.Common.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Credito.Framework.MongoDB.Serializers
{
    public class NumeroParcelaSerializer : SerializerBase<NumeroParcela>
    {
        public override NumeroParcela Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(int));
            var data = serializer.Deserialize(context, args);
            return (NumeroParcela)Activator.CreateInstance(
                typeof(NumeroParcela),
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new object[] { (int)data },
                null);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, NumeroParcela value)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(int));
            serializer.Serialize(context, value.ToInt());
        }
    }
}