using Credito.Application.v1_0.Models;
using MediatR;

namespace Credito.Application.v1_0.ContratoDeEmprestimo.Commands
{
    public record CalcularContratoCmd : CalcularContratoCmdBase, IRequest<ContratoDeEmprestimoModel>
    {

    }
}