using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Credito.Application.v1_0.ContratoDeEmprestimo.Queries;
using Credito.Application.v1_0.Models;

namespace Credito.Application.v1_0.ContratoDeEmprestimo.QueryHandlers.QueryImplementations.Interfaces
{
    public interface IObterContratos
    {
        Task<IEnumerable<ContratoDeEmprestimoModel>> ObterContratosAsync(ObterContratosQuery request,
                                                                         CancellationToken cancellationToken = default);
    }
}