using System.Threading;
using System.Threading.Tasks;
using Credito.Application.Models;
using MediatR;
using Credito.Application.ContratoDeEmprestimo.Queries;
using Credito.Application.Common.Exceptions;
using Credito.Application.ContratoDeEmprestimo.QueryHandlers.QueryImplementations.Interfaces;

namespace Credito.Application.ContratoDeEmprestimo.QueryHandlers
{
    public class ObterContratoPorIdHandler : IRequestHandler<ObterContratoPorIdQuery, ContratoDeEmprestimoModel>
    {
        private readonly IObterContratoPorId _obterContratoPorId;

        public ObterContratoPorIdHandler(IObterContratoPorId obterContratoPorId) =>
            _obterContratoPorId = obterContratoPorId;

        public async Task<ContratoDeEmprestimoModel> Handle(ObterContratoPorIdQuery request,
                                                            CancellationToken cancellationToken = default)
        {           
            var resource = _obterContratoPorId.ObterContratoPorId(request);

            if (resource == null)
                throw new ResourceNotFoundException(request);

            return await Task.FromResult(resource);
        }
    }
}