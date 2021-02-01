using System;
using MediatR;

namespace Credito.Application.v1_0.ContratoDeEmprestimo.Commands
{
    public record CriarContratoCmd : CalcularContratoCmdBase, IRequest
    {
        public Guid Id { get; init; }
    }
}