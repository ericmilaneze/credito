using System.Collections.Generic;
using System.Threading.Tasks;
using Credito.Application.v1_0.ContratoDeEmprestimo.Queries;
using Credito.Application.v1_0.ContratoDeEmprestimo.QueryHandlers;
using Credito.Application.v1_0.ContratoDeEmprestimo.QueryHandlers.QueryImplementations.Interfaces;
using Credito.Application.v1_0.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Credito.Application.v1_0.Tests.ContratoDeEmprestimo.QueryHandlers
{
    public class QryHandler_ObterContratos
    {
        [Fact]
        public async Task QryHandler_ObterContratos_Simples()
        {
            var obterContratos = Substitute.For<IObterContratos>();
            obterContratos.ObterContratosAsync(Arg.Any<ObterContratosQuery>())
                .Returns(new List<ContratoDeEmprestimoModel>());

            var handler = new ObterContratosHandler(obterContratos);
            var result = await handler.Handle(new ObterContratosQuery());

            await obterContratos.Received(1)
                                .ObterContratosAsync(Arg.Any<ObterContratosQuery>());
            result.Should().HaveCount(0);
        }
    }
}