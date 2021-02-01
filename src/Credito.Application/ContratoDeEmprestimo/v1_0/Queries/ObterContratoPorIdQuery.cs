using System;
using Credito.Application.v1_0.Models;
using MediatR;

namespace Credito.Application.v1_0.ContratoDeEmprestimo.Queries
{
    public record ObterContratoPorIdQuery : IRequest<ContratoDeEmprestimoModel>
    {
        public Guid Id { get; init; }
    }
}