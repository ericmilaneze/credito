// using System;
// using System.Threading.Tasks;
// using Credito.Domain.ContratoDeEmprestimo;
// using Xunit;
// using static Credito.Domain.ContratoDeEmprestimo.ContratoDeEmprestimoAggregate;
// using Credito.Framework.MongoDB;

// namespace Credito.Repository.Tests
// {
//     public class Testes_Repository
//     {
//         [Fact]
//         public async Task Testar_repository_de_contrato_de_emprestimo()
//         {
//             var id = Guid.NewGuid();

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
//             var mongoRepository = new MongoDbRepository<ContratoDeEmprestimoAggregate>(context);

//             var repository = new ContratoDeEmprestimoRepository(mongoRepository);

//             await repository.SaveAsync(contrato);

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

//             await repository.SaveAsync(contratoComMesmoIdDoAnterior);

//             var aggUpdated = await repository.LoadAsync(id);

//             await repository.RemoveAsync(id);

//             await repository.SaveAsync(contrato);

//             var aggs0 = await repository.FindAsync(x => x.Id == id);
//             var aggs2 = await repository.FindAsync(x => x.QuantidadeDeParcelas == 24, 0, 1);
//             var aggs3 = await repository.FindAsync(x => x.QuantidadeDeParcelas == 24, 1, 1);
            
//             var aggs4 = await repository.GetAsync(0, 1);
//             var aggs5 = await repository.GetAsync(0, 2);
//             var aggs6 = await repository.GetAsync(1, 1);
//         }
//     }
// }
