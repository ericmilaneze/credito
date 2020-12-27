using Credito.Domain.Common.ValueObjects;

namespace Credito.Domain.ContratoDeEmprestimo.CalculadoraDeParcela
{
    public class CalculadoraDeProximaParcela : ICalculadoraDeParcelaStrategy
    {
        public ValorMonetario CalcularSaldoDevedorInicial(ContratoDeEmprestimoAggregate contrato, NumeroParcela numeroParcela) =>
            contrato.ObterParcela(numeroParcela - 1).SaldoDevedorFinal;

        public ValorMonetario CalcularJuros(ContratoDeEmprestimoAggregate contrato, NumeroParcela numeroParcela) =>
            contrato.TaxaAoMes.Aplicar(CalcularSaldoDevedorInicial(contrato, numeroParcela));

        public decimal CalcularPrincipal(ContratoDeEmprestimoAggregate contrato, NumeroParcela numeroParcela) =>
            contrato.ValorDaParcela - CalcularJuros(contrato, numeroParcela);
    }
}