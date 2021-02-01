using System.Threading;
using System.Threading.Tasks;
using Credito.Application.v1_0.Models;
using MediatR;
using Credito.Application.v1_0.ContratoDeEmprestimo.Queries;
using System.Collections.Generic;
using Credito.Application.v1_0.ContratoDeEmprestimo.QueryHandlers.QueryImplementations.Interfaces;

namespace Credito.Application.v1_0.ContratoDeEmprestimo.QueryHandlers
{
    public class ObterContratosHandler : IRequestHandler<ObterContratosQuery, IEnumerable<ContratoDeEmprestimoModel>>
    {
        private readonly IObterContratos _obterContratos;

        public ObterContratosHandler(IObterContratos obterContratos) =>
            _obterContratos = obterContratos;

        public async Task<IEnumerable<ContratoDeEmprestimoModel>> Handle(ObterContratosQuery request,
                                                                         CancellationToken cancellationToken = default) =>
            await _obterContratos.ObterContratosAsync(request);
    }
}