using Credito.Application.v1_0.ContratoDeEmprestimo.Queries;
using Credito.Application.v1_0.Models;

namespace Credito.Application.v1_0.ContratoDeEmprestimo.QueryHandlers.QueryImplementations.Interfaces
{
    public interface IObterContratoPorId
    {
        ContratoDeEmprestimoModel ObterContratoPorId(ObterContratoPorIdQuery request);
    }
}