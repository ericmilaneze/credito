using System;
using MediatR;

namespace Credito.Application.ContratoDeEmprestimo.Commands
{
    public record CriarContratoCmd : CalcularContratoCmdBase, IRequest
    {

    }
}