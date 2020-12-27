using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Credito.Framework.MongoDB
{
    public class IdadeSerializer : SerializerBase<Idade>
    {
        public override Idade Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(int));
            var data = serializer.Deserialize(context, args);
            return new Idade((int)data);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Idade value)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(int));
            serializer.Serialize(context, value.ToInt());
        }
    }
}