using Credito.Application.Models;
using MediatR;

namespace Credito.Application.ContratoDeEmprestimo.Commands
{
    public record CalcularContratoCmd : CalcularContratoCmdBase, IRequest<ContratoDeEmprestimoModel>
    {

    }
}