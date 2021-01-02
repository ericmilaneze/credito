using System;
using Credito.Application.Models;
using MediatR;

namespace Credito.Application.ContratoDeEmprestimo.Queries
{
    public record ObterContratoPorIdQuery : IRequest<ContratoDeEmprestimoModel>
    {
        public Guid Id { get; init; }
    }
}