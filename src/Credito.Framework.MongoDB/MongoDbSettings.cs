using Credito.Framework.MongoDB.Registry;

namespace Credito.Framework.MongoDB
{
    public class MongoDbSettings
    {
        public static void RegiterSettings()
        {
            ConventionsRegistry.RegisterConventions();
            SerializersRegistry.RegisterSerializers();
            ClassMapsRegistry.RegisterClassMaps();
        }
    }
}