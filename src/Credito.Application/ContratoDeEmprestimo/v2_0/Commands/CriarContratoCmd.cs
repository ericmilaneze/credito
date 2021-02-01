using System;
using MediatR;

namespace Credito.Application.v2_0.ContratoDeEmprestimo.Commands
{
    public record CriarContratoCmd : CalcularContratoCmdBase, IRequest
    {
        public Guid Id { get; init; }
        public Guid ClienteId { get; set; }
    }
}