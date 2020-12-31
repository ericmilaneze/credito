// using System;
// using System.Threading.Tasks;
// using Credito.Domain.ContratoDeEmprestimo;
// using Credito.Framework.MongoDB.Example;
// using Xunit;
// using static Credito.Domain.ContratoDeEmprestimo.ContratoDeEmprestimoAggregate;
// using Credito.Framework.MongoDB.ContratoDeEmprestimo;
// using Credito.Framework.MongoDB.Registry;
// using MongoDB.Bson;
// using Newtonsoft.Json;
// using MongoDB.Driver;
// using System.Linq;
// using Credito.Framework.MongoDB.Tests.Models;

// namespace Credito.Framework.MongoDB.Tests
// {
//     public class Testar_MongoDb
//     {
//         [Fact]
//         public async Task Repositorio_de_contratos()
//         {
//             var id = Guid.NewGuid();
//             //var id = Guid.Parse("22c66486-9c99-44a8-b5d5-1be482207b01");

//             var contrato = ContratoDeEmprestimoAggregate.CriarContrato(
//                 new ParametrosDeContratoDeEmprestimo
//                 {
//                     Id = id,
//                     ValorLiquido = 3000M,
//                     Prazo = 24,
//                     TaxaAoMes = 5.00M,
//                     Tac = 6.00M,
//                     Iof = 10.00M,
//                     DiasDeCarencia = 30
//                 });

//             var context = new MongoDbContext("mongodb://localhost", "test");
//             var repository = new ContratoDeEmprestimoRepository(context);

//             await repository.InsertAsync(contrato);

//             var agg = await repository.LoadAsync(id);

//             var contratoComMesmoIdDoAnterior = ContratoDeEmprestimoAggregate.CriarContrato(
//                 new ParametrosDeContratoDeEmprestimo
//                 {
//                     Id = contrato.Id,
//                     ValorLiquido = 5000M,
//                     Prazo = 12,
//                     TaxaAoMes = 5.00M,
//                     Tac = 12.00M,
//                     Iof = 20.00M,
//                     DiasDeCarencia = 30
//                 });

//             await repository.UpdateAsync(contratoComMesmoIdDoAnterior);

//             var aggUpdated = await repository.LoadAsync(id);

//             await repository.RemoveAsync(id);

//             await repository.InsertAsync(contrato);

//             var aggs0 = await repository.FindAsync(x => x.Id == id);
//             var aggs2 = await repository.FindAsync(x => x.QuantidadeDeParcelas == 24, 0, 1);
//             var aggs3 = await repository.FindAsync(x => x.QuantidadeDeParcelas == 24, 1, 1);
            
//             var aggs4 = await repository.GetAsync(0, 1);
//             var aggs5 = await repository.GetAsync(0, 2);
//             var aggs6 = await repository.GetAsync(1, 1);
//         }

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
