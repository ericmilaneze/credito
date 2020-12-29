using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Credito.Domain.Common;
using Credito.Domain.ContratoDeEmprestimo;
using Credito.Framework.MongoDB.Example;
using Credito.Framework.MongoDB.Serializers;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Credito.Framework.MongoDB
{
    public class MongoDbRepository
    {
        private readonly IMongoDatabase _database;

        public MongoDbRepository(string connectionString, string dbName)
        {
            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase(dbName);
            
            RegisterConventions();
            RegisterSerializers();
            RegisterClassMaps();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("Custom Conventions",
                                        new ConventionPack
                                        {
                                            new CamelCaseElementNameConvention(),
                                            new IgnoreIfNullConvention(true),
                                            new EnumRepresentationConvention(BsonType.String)
                                        },
                                        _ => true);
        }

        private void RegisterSerializers()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DecimalSerializer(BsonType.Decimal128));

            BsonSerializer.RegisterSerializer(new IdadeExampleSerializer());
            BsonSerializer.RegisterSerializer(new PercentualPositivoSerializer());
            BsonSerializer.RegisterSerializer(new PercentualSerializer());
            BsonSerializer.RegisterSerializer(new PrazoSerializer());
            BsonSerializer.RegisterSerializer(new ValorMonetarioPositivoSerializer());
            BsonSerializer.RegisterSerializer(new ValorMonetarioSerializer());
            BsonSerializer.RegisterSerializer(new NumeroParcelaSerializer());
        }

        private void RegisterClassMaps()
        {
            BsonClassMap.RegisterClassMap<ExampleAggregate>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<ContratoDeEmprestimoAggregate>(cm =>
            {
                cm.AutoMap();
                cm.MapField("_parcelas").SetElementName("parcelas");
                cm.MapProperty(c => c.TaxaAoDia).SetElementName("taxaAoDia");
                cm.MapProperty(c => c.ValorCarencia).SetElementName("valorCarencia");
                cm.MapProperty(c => c.ValorFinanciado).SetElementName("valorFinanciado");
                cm.MapProperty(c => c.ValorDaParcela).SetElementName("valorDaParcela");
                cm.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<Parcela>(cm =>
            {
                cm.AutoMap();
                cm.MapField(p => p.SaldoDevedorFinal).SetElementName("saldoDevedorFinal");
                cm.SetIgnoreExtraElements(true);
            });
        }

        public IMongoCollection<T> GetCollection<T>() =>
            _database.GetCollection<T>(GetCollectionName<T>());

        public string GetCollectionName<T>()
        {
            switch (typeof(T))
            {
                case Type t when t == typeof(ExampleAggregate):
                    return "example_aggregates";
                case Type t when t == typeof(ContratoDeEmprestimoAggregate):
                    return "contratos";
                default:
                    throw new NotImplementedException($"Collection name not set yet for \"{typeof(T).Name}\"");
            }
        }

        public T Load<T>(Guid id) where T : AggregateRoot =>
            GetCollection<T>().AsQueryable()
                              .Where(x => x.Id == id)
                              .FirstOrDefault();

        public IList<T> Find<T>(Expression<Func<T, bool>> filter,
                                int skip = 0,
                                int take = 10) =>
            GetCollection<T>().AsQueryable()
                              .Where(filter)
                              .Skip(skip)
                              .Take(take)
                              .ToList();

        public IList<T> Get<T>(int skip = 0, int take = 10) =>
            GetCollection<T>().AsQueryable()
                              .Skip(skip)
                              .Take(take)
                              .ToList();

        public void Insert<T>(T aggregate) =>
            GetCollection<T>().InsertOne(aggregate);

        public void Update<T>(T aggregate) where T : AggregateRoot =>
            GetCollection<T>().ReplaceOne(x => x.Id == aggregate.Id, aggregate);

        public void Remove<T>(Guid id) where T : AggregateRoot =>
            GetCollection<T>().DeleteOne(x => x.Id == id);
    }
}