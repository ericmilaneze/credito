// using System.Threading.Tasks;
// using Xunit;
// using MongoDB.Bson;
// using MongoDB.Driver;
// using System.Linq;
// using Credito.Framework.MongoDB.Tests.Models;

// namespace Credito.Framework.MongoDB.Tests
// {
//     public class Testes_MongoDb
//     {
//         [Fact]
//         public async Task MongoDb_Context()
//         {
//             var context = new MongoDbContext("mongodb://localhost", "test");

//             FilterDefinition<BsonDocument> filter = new BsonDocument { { new BsonElement("_id", "1234") } };           
//             var bsonContrato = BsonDocument.Parse("{ \"_id\": \"1234\", \"nome\": \"Eric Milaneze\", \"idade\": 34 }");
//             context.GetCollection("test_collection").ReplaceOne(filter, bsonContrato, new ReplaceOptions() { IsUpsert = true });

//             PipelineDefinition<BsonDocument, dynamic> pipeline = 
//                 new[] 
//                 {
//                     new BsonDocument
//                     {
//                         {
//                             "$match",
//                             new BsonDocument
//                             {
//                                 { new BsonElement("_id", "1234") }
//                             }
//                         }
//                     }
//                 };
//             var cursor = await context.GetCollection("test_collection").AggregateAsync(pipeline, new AggregateOptions());
//             var result = cursor.ToList();
//             var first = result.First();

//             var id = first._id;
//             var nome = first.nome;
//             var idade = first.idade;
//         }

//         [Fact]
//         public async Task MongoDb_Context2()
//         {
//             var context = new MongoDbContext("mongodb://localhost", "test");

//             FilterDefinition<BsonDocument> filter = new BsonDocument { { new BsonElement("_id", "1234") } };           
//             var bsonContrato = BsonDocument.Parse("{ \"_id\": \"1234\", \"nome\": \"Eric Milaneze\", \"idade\": 34 }");
//             context.GetCollection("test_collection").ReplaceOne(filter, bsonContrato, new ReplaceOptions() { IsUpsert = true });

//             var cursor = await context.GetCollection("test_collection").FindAsync(filter);
//             var result = cursor.ToList();
//             var first = result.First();

//             var id = first.GetValue("_id").ToString();
//             var nome = first.GetValue("nome").ToString();
//             var idade = first.GetValue("idade").ToInt32();
//         }

//         [Fact]
//         public void MongoDb_Context3()
//         {
//             var context = new MongoDbContext("mongodb://localhost", "test");

//             FilterDefinition<BsonDocument> filter = new BsonDocument { { new BsonElement("_id", "1234") } };           
//             var bsonContrato = BsonDocument.Parse("{ \"_id\": \"1234\", \"nome\": \"Eric Milaneze\", \"idade\": 34 }");
//             context.GetCollection("test_collection").ReplaceOne(filter, bsonContrato, new ReplaceOptions() { IsUpsert = true });

//             var first = context.GetCollection<Pessoa>("test_collection").AsQueryable().Where(x => x.Id == "1234").First();
//         }

//         [Fact]
//         public async Task MongoDb_Context4()
//         {
//             var context = new MongoDbContext("mongodb://localhost", "test");

//             FilterDefinition<BsonDocument> filter = new BsonDocument { { new BsonElement("_id", "1234") } };           
//             var bsonContrato = BsonDocument.Parse("{ \"_id\": \"1234\", \"nome\": \"Eric Milaneze\", \"idade\": 34 }");
//             context.GetCollection("test_collection").ReplaceOne(filter, bsonContrato, new ReplaceOptions() { IsUpsert = true });

//             var result = await context.GetCollection<Pessoa>("test_collection")
//                                       .Aggregate()
//                                       .Limit(1)
//                                       .Skip(0)
//                                       .Project(x => new { NomeDaPessoa = x.Nome, IdadeDaPessoa = x.Idade })
//                                       .ToListAsync();
//         }

//         [Fact]
//         public async Task MongoDb_Context5()
//         {
//             var context = new MongoDbContext("mongodb://localhost", "test");

//             FilterDefinition<BsonDocument> filter = new BsonDocument { { new BsonElement("_id", "1234") } };           
//             var bsonContrato = BsonDocument.Parse("{ \"_id\": \"1234\", \"nome\": \"Eric Milaneze\", \"idade\": 34 }");
//             context.GetCollection("test_collection").ReplaceOne(filter, bsonContrato, new ReplaceOptions() { IsUpsert = true });

//             var result = await context.GetCollection<Pessoa>("test_collection")
//                                       .Find(x => x.Id == "1234")
//                                       .Limit(1)
//                                       .Skip(0)
//                                       .Project(x => new { NomeDaPessoa = x.Nome, IdadeDaPessoa = x.Idade })
//                                       .ToListAsync();
//         }
//     }
// }
