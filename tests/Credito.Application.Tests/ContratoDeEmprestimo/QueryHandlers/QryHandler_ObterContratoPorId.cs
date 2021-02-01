using System.Threading.Tasks;
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
            var obterContratoPorId = Substitute.For<IObterContratoPorId>();
            obterContratoPorId.ObterContratoPorId(Arg.Any<ObterContratoPorIdQuery>())
                              .Returns(new ContratoDeEmprestimoModel());

            var handler = new ObterContratoPorIdHandler(obterContratoPorId);
            var result = await handler.Handle(new ObterContratoPorIdQuery());

            obterContratoPorId.Received(1)
                              .ObterContratoPorId(Arg.Any<ObterContratoPorIdQuery>());
            result.Should().BeEquivalentTo(new ContratoDeEmprestimoModel());
        }

        [Fact]
        public async Task QryHandler_ObterContratoPorId_Inexistente()
        {
            var obterContratoPorId = Substitute.For<IObterContratoPorId>();
            obterContratoPorId.ObterContratoPorId(Arg.Any<ObterContratoPorIdQuery>())
                              .Returns((ContratoDeEmprestimoModel)null);

            var handler = new ObterContratoPorIdHandler(obterContratoPorId);           
            
            await FluentActions
                .Invoking(
                    async() =>
                    {
                        var result = await handler.Handle(new ObterContratoPorIdQuery());
                    })
                .Should()
                .ThrowAsync<ResourceNotFoundException>();

            obterContratoPorId.Received(1)
                              .ObterContratoPorId(Arg.Any<ObterContratoPorIdQuery>());
        }
    }
}