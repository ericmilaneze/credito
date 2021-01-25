using Credito.Application.ContratoDeEmprestimo.Queries;
using Credito.Application.Models;

namespace Credito.Application.ContratoDeEmprestimo.QueryHandlers.QueryImplementations.Interfaces
{
    public interface IObterContratoPorId
    {
        ContratoDeEmprestimoModel ObterContratoPorId(ObterContratoPorIdQuery request);
    }
}