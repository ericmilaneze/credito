using System.Collections.Generic;
using Credito.Application.v1_0.Models;
using MediatR;

namespace Credito.Application.v1_0.ContratoDeEmprestimo.Queries
{
    public record ObterContratosQuery : IRequest<IEnumerable<ContratoDeEmprestimoModel>>
    {
        public int Skip { get; init; }
        public int Take { get; init; } = 20;
    }
}