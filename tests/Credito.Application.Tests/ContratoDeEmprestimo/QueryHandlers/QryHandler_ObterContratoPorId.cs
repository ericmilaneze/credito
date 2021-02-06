using System.Threading.Tasks;
using AutoFixture;
using Credito.Application.Common.Exceptions;
using Credito.Application.v1_0.ContratoDeEmprestimo.Queries;
using Credito.Application.v1_0.ContratoDeEmprestimo.QueryHandlers;
using Credito.Application.v1_0.ContratoDeEmprestimo.QueryHandlers.QueryImplementations.Interfaces;
using Credito.Application.v1_0.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Credito.Application.v1_0.Tests.ContratoDeEmprestimo.QueryHandlers
{
    public class QryHandler_ObterContratoPorId
    {
        [Fact]
        public async Task QryHandler_ObterContratoPorId_Simples()
        {
            var fixture = new Fixture();

            var returnThis = fixture.Create<ContratoDeEmprestimoModel>();

            var obterContratoPorId = Substitute.For<IObterContratoPorId>();
            obterContratoPorId.ObterContratoPorId(Arg.Any<ObterContratoPorIdQuery>())
                              .Returns(returnThis);

            var handler = new ObterContratoPorIdHandler(obterContratoPorId);
            var result = await handler.Handle(fixture.Create<ObterContratoPorIdQuery>());

            obterContratoPorId.Received(1)
                              .ObterContratoPorId(Arg.Any<ObterContratoPorIdQuery>());
            result.Should().BeEquivalentTo(returnThis);
        }

        [Fact]
        public async Task QryHandler_ObterContratoPorId_Inexistente()
        {
            var fixture = new Fixture();

            var obterContratoPorId = Substitute.For<IObterContratoPorId>();
            obterContratoPorId.ObterContratoPorId(Arg.Any<ObterContratoPorIdQuery>())
                              .Returns((ContratoDeEmprestimoModel)null);

            var handler = new ObterContratoPorIdHandler(obterContratoPorId);           
            
            await FluentActions
                .Invoking(async() => await handler.Handle(new ObterContratoPorIdQuery()))
                .Should()
                .ThrowAsync<ResourceNotFoundException>();

            obterContratoPorId.Received(1)
                              .ObterContratoPorId(Arg.Any<ObterContratoPorIdQuery>());
        }
    }
}