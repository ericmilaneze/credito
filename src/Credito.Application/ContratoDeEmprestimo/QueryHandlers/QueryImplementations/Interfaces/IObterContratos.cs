using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Credito.Application.ContratoDeEmprestimo.Queries;
using Credito.Application.Models;

namespace Credito.Application.ContratoDeEmprestimo.QueryHandlers.QueryImplementations.Interfaces
{
    public interface IObterContratos
    {
        Task<IEnumerable<ContratoDeEmprestimoModel>> ObterContratosAsync(ObterContratosQuery request,
                                                                         CancellationToken cancellationToken = default);
    }
}