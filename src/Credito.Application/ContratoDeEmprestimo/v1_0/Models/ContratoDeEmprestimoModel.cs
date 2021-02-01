using System;
using System.Collections.Generic;

namespace Credito.Application.v1_0.Models
{
    public record ContratoDeEmprestimoModel
    {
        public Guid Id { get; init; }
        public decimal ValorLiquido { get; init; }
        public int QuantidadeDeParcelas { get; init; }
        public decimal TaxaAoMes { get; init; }
        public decimal Tac { get; init; }
        public decimal Iof { get; init; }
        public int DiasDeCarencia { get; init; }
        public decimal TaxaAoDia { get; init; }
        public decimal ValorCarencia { get; init; }
        public decimal ValorFinanciado { get; init; }
        public decimal ValorDaParcela { get; init; }
        public IList<ParcelaModel> Parcelas { get; init; }
    }
}