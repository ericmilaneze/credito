using System;
using System.Threading.Tasks;
using Credito.Domain.ContratoDeEmprestimo;
using Credito.Framework.MongoDB.Example;
using Xunit;
using static Credito.Domain.ContratoDeEmprestimo.ContratoDeEmprestimoAggregate;
using Credito.Framework.MongoDB.ContratoDeEmprestimo;

namespace Credito.Framework.MongoDB.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task TestExample()
        {
            var id = Guid.NewGuid();
            //var id = "02089b09-5ad2-4dd8-a569-cc086d76d4dc";

            var mongo = new MongoDbRepository("mongodb://localhost", "test");
            await mongo.InsertAsync(ExampleAggregate.CreateExampleAggregate(id, "Eric", 34, 100000M));

            var agg = await mongo.LoadAsync<ExampleAggregate>(id);

            await mongo.UpdateAsync(ExampleAggregate.CreateExampleAggregate(id, "Eric", 35, 100000M));

            var aggUpdated = await mongo.LoadAsync<ExampleAggregate>(id);

            await mongo.RemoveAsync<ExampleAggregate>(id);

            await mongo.InsertAsync(ExampleAggregate.CreateExampleAggregate(id, "Eric", 28, 100000M));

            var aggs0 = await mongo.FindAsync<ExampleAggregate>(x => x.Id == id);
            var aggs2 = await mongo.FindAsync<ExampleAggregate>(x => x.Idade == 28, 0, 1);
            var aggs3 = await mongo.FindAsync<ExampleAggregate>(x => x.Idade == 28, 1, 1);
            
            var aggs4 = await mongo.GetAsync<ExampleAggregate>(0, 1);
            var aggs5 = await mongo.GetAsync<ExampleAggregate>(0, 2);
            var aggs6 = await mongo.GetAsync<ExampleAggregate>(1, 1);
        }

        [Fact]
        public async Task TestContrato()
        {
            var id = Guid.NewGuid();
            //var id = Guid.Parse("22c66486-9c99-44a8-b5d5-1be482207b01");

            var contrato = ContratoDeEmprestimoAggregate.CriarContrato(
                new ParametrosDeContratoDeEmprestimo
                {
                    Id = id,
                    ValorLiquido = 3000M,
                    Prazo = 24,
                    TaxaAoMes = 5.00M,
                    Tac = 6.00M,
                    Iof = 10.00M,
                    DiasDeCarencia = 30
                });

            var mongo = new MongoDbRepository("mongodb://localhost", "test");
            await mongo.InsertAsync(contrato);

            var agg = await mongo.LoadAsync<ContratoDeEmprestimoAggregate>(id);
        }

        [Fact]
        public async Task TestContratoRepository()
        {
            var id = Guid.NewGuid();
            //var id = Guid.Parse("22c66486-9c99-44a8-b5d5-1be482207b01");

            var contrato = ContratoDeEmprestimoAggregate.CriarContrato(
                new ParametrosDeContratoDeEmprestimo
                {
                    Id = id,
                    ValorLiquido = 3000M,
                    Prazo = 24,
                    TaxaAoMes = 5.00M,
                    Tac = 6.00M,
                    Iof = 10.00M,
                    DiasDeCarencia = 30
                });

            var mongo = new MongoDbRepository("mongodb://localhost", "test");
            var contratoRepository = new ContratoDeEmprestimoRepository(mongo);
            await contratoRepository.InsertAsync(contrato);

            var agg = await contratoRepository.LoadAsync(id);
        }
    }
}
