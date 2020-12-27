using Credito.Domain.Common.ValueObjects;

namespace Credito.Domain.ContratoDeEmprestimo.CalculadoraDeParcela
{
    public class CalculadoraDePrimeiraParcela : ICalculadoraDeParcelaStrategy
    {
        public ValorMonetario CalcularSaldoDevedorInicial(ContratoDeEmprestimoAggregate contrato, NumeroParcela numeroParcela) =>
            contrato.ValorCarencia + contrato.ValorFinanciado;

        public ValorMonetario CalcularJuros(ContratoDeEmprestimoAggregate contrato, NumeroParcela numeroParcela) =>
            contrato.TaxaAoMes.Aplicar(contrato.ValorFinanciado);

        public decimal CalcularPrincipal(ContratoDeEmprestimoAggregate contrato, NumeroParcela numeroParcela) =>
            contrato.ValorDaParcela - CalcularJuros(contrato, numeroParcela) + contrato.ValorCarencia;
    }
}