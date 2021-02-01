using System;
using MediatR;

namespace Credito.Application.ContratoDeEmprestimo.Commands
{
    public record CriarContratoCmd : CalcularContratoCmdBase, IRequest
    {
        public Guid Id { get; init; }
    }

    public record CriarContratoCmdV2 : CalcularContratoCmdBase, IRequest
    {
        public Guid Id { get; init; }
        public Guid ClienteId { get; set; }
    }
}