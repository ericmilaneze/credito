using System;
using Credito.Domain.ContratoDeEmprestimo;
using MediatR;

namespace Credito.Application.ContratoDeEmprestimo.Commands
{
    public record CalcularContratoCmd : CalcularContratoCmdBase, IRequest<ContratoDeEmprestimoAggregate>
    {

    }
}