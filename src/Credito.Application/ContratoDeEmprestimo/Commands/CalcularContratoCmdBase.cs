using System;

namespace Credito.Application.ContratoDeEmprestimo.Commands
{
    public record CalcularContratoCmdBase
    {
        public decimal ValorLiquido { get; init; }
        public int QuantidadeDeParcelas { get; init; }
        public decimal TaxaAoMes { get; init; }
        public decimal Tac { get; init; }
        public decimal Iof { get; init; }
        public int DiasDeCarencia { get; init; }
    }
}