using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using Credito.Application.v1_0.ContratoDeEmprestimo.CommandHandlers;
using Credito.Application.v1_0.ContratoDeEmprestimo.Commands;
using Credito.Application.v1_0.Models;
using Credito.Domain.ContratoDeEmprestimo;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Credito.Application.v1_0.Tests.ContratoDeEmprestimo.CommandHandlers
{
    public class CmdHandler_Calcular_Contrato
    {
        [Theory, AutoData]
        public async Task CdmHandler_CalcularContrato_Simples(CalcularContratoCmd cmd)
        {
            var fixture = new Fixture();
            
            var returnThis = fixture.Create<ContratoDeEmprestimoModel>();

            var mapper = Substitute.For<IMapper>();
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