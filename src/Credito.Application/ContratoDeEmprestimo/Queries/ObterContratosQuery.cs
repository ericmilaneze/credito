using System.Collections.Generic;
using Credito.Application.Models;
using MediatR;

namespace Credito.Application.ContratoDeEmprestimo.Queries
{
    public record ObterContratosQuery : IRequest<IEnumerable<ContratoDeEmprestimoModel>>
    {
        public int Skip { get; init; }
        public int Take { get; init; } = 20;
    }
}