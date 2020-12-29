using Credito.Framework.MongoDB.Example;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Credito.Framework.MongoDB.Serializers
{
    public class IdadeExampleSerializer : SerializerBase<IdadeExample>
    {
        public override IdadeExample Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(int));
            var data = serializer.Deserialize(context, args);
            return new IdadeExample((int)data);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, IdadeExample value)
        {
            var serializer = BsonSerializer.LookupSerializer(typeof(int));
            serializer.Serialize(context, value.ToInt());
        }
    }
}