using System.Collections.Generic;
using Credito.Domain.ContratoDeEmprestimo;

namespace Credito.Domain.Tests.DataAttributes
{
    public record ContratoData
    {
        public decimal ValorLiquido { get; init; }
        public int Prazo { get; init; }
        public decimal TaxaAoMes { get; init; }
        public decimal Tac { get; init; }
        public decimal Iof { get; init; }
        public int DiasDeCarencia { get; init; }
        public decimal TaxaAoDia { get; init; }
        public decimal ValorCarencia { get; init; }
        public decimal ValorFinanciado { get; init; }
        public decimal ValorDaParcela { get; init; }
        public IList<Parcela> Parcelas { get; init; }
    }
}