using System;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
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
        [Theory, AutoData]
        public async Task CmdHandler_CriarContrato_Simples(CriarContratoCmd cmd)
        {
            var repository = Substitute.For<IContratoDeEmprestimoRepository>();
            repository.LoadAsync(Arg.Any<Guid>())
                .ReturnsForAnyArgs((ContratoDeEmprestimoAggregate)null);

            IRequestHandler<CriarContratoCmd> handler = new CriarContratoHandler(repository);
            await handler.Handle(cmd, CancellationToken.None);

            await repository.Received(1)
                            .LoadAsync(Arg.Any<Guid>());
            await repository.Received(1)
                            .SaveAsync(Arg.Any<ContratoDeEmprestimoAggregate>());
        }

        [Fact]
        public async Task CmdHandler_CriarContrato_Ja_Existente()
        {
            var fixture = new Fixture();

            var repository = Substitute.For<IContratoDeEmprestimoRepository>();
            repository.LoadAsync(Arg.Any<Guid>())
                .ReturnsForAnyArgs(fixture.Create<ContratoDeEmprestimoAggregate>());

            IRequestHandler<CriarContratoCmd> handler = new CriarContratoHandler(repository);

            await FluentActions
                .Invoking(async () => 
                    await handler.Handle(fixture.Create<CriarContratoCmd>(), CancellationToken.None))
                .Should()
                .ThrowAsync<ResourceAlreadyExistsException>();

            await repository.Received(1)
                            .LoadAsync(Arg.Any<Guid>());
            await repository.Received(0)
                            .SaveAsync(Arg.Any<ContratoDeEmprestimoAggregate>());
        }
    }
}