// using System;
// using Credito.Domain.ContratoDeEmprestimo;
// using Credito.Framework.MongoDB.Example;
// using Xunit;
// using static Credito.Domain.ContratoDeEmprestimo.ContratoDeEmprestimoAggregate;

// namespace Credito.Framework.MongoDB.Tests
// {
//     public class UnitTest1
//     {
//         [Fact]
//         public void TestExample()
//         {
//             var id = Guid.NewGuid();
//             //var id = "02089b09-5ad2-4dd8-a569-cc086d76d4dc";

//             var mongo = new MongoDbRepository("mongodb://localhost", "test");
//             mongo.Insert(ExampleAggregate.CreateExampleAggregate(id, "Eric", 34, 100000M));

//             var agg = mongo.Load<ExampleAggregate>(id);

//             mongo.Update(ExampleAggregate.CreateExampleAggregate(id, "Eric", 35, 100000M));

//             var aggUpdated = mongo.Load<ExampleAggregate>(id);

//             mongo.Remove<ExampleAggregate>(id);

//             mongo.Insert(ExampleAggregate.CreateExampleAggregate(id, "Eric", 28, 100000M));

//             var aggs0 = mongo.Find<ExampleAggregate>(x => x.Id == id);
//             var aggs2 = mongo.Find<ExampleAggregate>(x => x.Idade == 28, 0, 1);
//             var aggs3 = mongo.Find<ExampleAggregate>(x => x.Idade == 28, 1, 1);
            
//             var aggs4 = mongo.Get<ExampleAggregate>(0, 1);
//             var aggs5 = mongo.Get<ExampleAggregate>(0, 2);
//             var aggs6 = mongo.Get<ExampleAggregate>(1, 1);
//         }

//         [Fact]
//         public void TestContrato()
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

//             var mongo = new MongoDbRepository("mongodb://localhost", "test");
//             mongo.Insert(contrato);

//             var agg = mongo.Load<ContratoDeEmprestimoAggregate>(id);
//         }
//     }
// }
