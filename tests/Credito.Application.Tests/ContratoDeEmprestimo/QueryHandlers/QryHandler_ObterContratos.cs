using System.Collections.Generic;
using System.Threading.Tasks;
using Credito.Application.ContratoDeEmprestimo.Queries;
using Credito.Application.ContratoDeEmprestimo.QueryHandlers;
using Credito.Application.ContratoDeEmprestimo.QueryHandlers.QueryImplementations.Interfaces;
using Credito.Application.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Credito.Application.Tests.ContratoDeEmprestimo.QueryHandlers
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