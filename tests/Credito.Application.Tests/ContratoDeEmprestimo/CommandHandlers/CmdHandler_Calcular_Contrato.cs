using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Credito.Application.ContratoDeEmprestimo.CommandHandlers;
using Credito.Application.ContratoDeEmprestimo.Commands;
using Credito.Application.Models;
using Credito.Domain.ContratoDeEmprestimo;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Credito.Application.Tests.ContratoDeEmprestimo.CommandHandlers
{
    public class CmdHandler_Calcular_Contrato
    {
        [Fact]
        public async Task CdmHandler_CalcularContrato_Simples()
        {
            var cmd =
                new CalcularContratoCmd
                {
                    ValorLiquido = 3000,
                    QuantidadeDeParcelas = 24,
                    TaxaAoMes = 5.00M,
                    Tac = 6.00M,
                    Iof = 10.00M,
                    DiasDeCarencia = 30
                };

            var mapper = Substitute.For<IMapper>();
            var returnThis = 
                new ContratoDeEmprestimoModel
                {
                    ValorLiquido = cmd.ValorLiquido,
                    QuantidadeDeParcelas = cmd.QuantidadeDeParcelas,
                    TaxaAoMes = cmd.TaxaAoMes,
                    Tac = cmd.Tac,
                    Iof = cmd.Iof,
                    DiasDeCarencia = cmd.DiasDeCarencia
                };
            mapper.Map<ContratoDeEmprestimoModel>(Arg.Any<ContratoDeEmprestimoAggregate>())
                  .Returns(returnThis);

            var handler = new CalcularContratoHandler(mapper);
            var result = await handler.Handle(cmd, CancellationToken.None);

            result.Should().BeEquivalentTo(returnThis);
            mapper.Received(1)
                  .Map<ContratoDeEmprestimoModel>(Arg.Any<ContratoDeEmprestimoAggregate>());
        }
    }
}