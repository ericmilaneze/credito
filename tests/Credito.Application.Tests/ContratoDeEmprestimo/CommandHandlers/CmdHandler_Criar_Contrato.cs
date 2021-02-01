using System;
using System.Threading;
using System.Threading.Tasks;
using Credito.Application.Common.Exceptions;
using Credito.Application.v1_0.ContratoDeEmprestimo.CommandHandlers;
using Credito.Application.v1_0.ContratoDeEmprestimo.Commands;
using Credito.Domain.ContratoDeEmprestimo;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Credito.Application.v1_0.Tests.ContratoDeEmprestimo.CommandHandlers
{
    public class CmdHandler_Criar_Contrato
    {
        [Fact]
        public async Task CmdHandler_CriarContrato_Simples()
        {
            var repository = Substitute.For<IContratoDeEmprestimoRepository>();
            repository.LoadAsync(Arg.Any<Guid>())
                .ReturnsForAnyArgs((ContratoDeEmprestimoAggregate)null);

            IRequestHandler<CriarContratoCmd> handler = new CriarContratoHandler(repository);
            await handler.Handle(
                new CriarContratoCmd
                {
                    Id = Guid.NewGuid(),
                    ValorLiquido = 3000,
                    QuantidadeDeParcelas = 24,
                    TaxaAoMes = 5.00M,
                    Tac = 6.00M,
                    Iof = 10.00M,
                    DiasDeCarencia = 30
                },
                CancellationToken.None);

            await repository.Received(1)
                            .LoadAsync(Arg.Any<Guid>());
            await repository.Received(1)
                            .SaveAsync(Arg.Any<ContratoDeEmprestimoAggregate>());
        }

        [Fact]
        public async Task CmdHandler_CriarContrato_Ja_Existente()
        {
            var repository = Substitute.For<IContratoDeEmprestimoRepository>();
            repository.LoadAsync(Arg.Any<Guid>())
                .ReturnsForAnyArgs(
                    ContratoDeEmprestimoAggregate.CriarContrato(new ContratoDeEmprestimoAggregate.ParametrosDeContratoDeEmprestimo()));

            IRequestHandler<CriarContratoCmd> handler = new CriarContratoHandler(repository);

            await FluentActions
                .Invoking(
                    async () =>
                        await handler.Handle(
                            new CriarContratoCmd
                            {
                                Id = Guid.NewGuid(),
                                ValorLiquido = 3000,
                                QuantidadeDeParcelas = 24,
                                TaxaAoMes = 5.00M,
                                Tac = 6.00M,
                                Iof = 10.00M,
                                DiasDeCarencia = 30
                            },
                            CancellationToken.None))
                .Should()
                .ThrowAsync<ResourceAlreadyExistsException>();

            await repository.Received(1)
                            .LoadAsync(Arg.Any<Guid>());
            await repository.Received(0)
                            .SaveAsync(Arg.Any<ContratoDeEmprestimoAggregate>());
        }
    }
}