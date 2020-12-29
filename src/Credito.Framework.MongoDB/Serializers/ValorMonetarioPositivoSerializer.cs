using System;
using System.Reflection;
using Credito.Domain.Common.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Credito.Framework.MongoDB.Serializers
{
    public class ValorMonetarioPositivoSerializer : SerializerBase<ValorMonetarioPositivo>
    {
        public override ValorMonetarioPositivo Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(decimal));
            var data = serializer.Deserialize(context, args);
            return (ValorMonetarioPositivo)Activator.CreateInstance(
                typeof(ValorMonetarioPositivo),
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new object[] { (decimal)data },
                null);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ValorMonetarioPositivo value)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(decimal));
            serializer.Serialize(context, value.ToDecimal());
        }
    }
}