using Credito.Domain.Common.ValueObjects;

namespace Credito.Domain.ContratoDeEmprestimo.CalculadoraDeParcela
{
    public interface ICalculadoraDeParcelaStrategy
    {
        ValorMonetario CalcularSaldoDevedorInicial(ContratoDeEmprestimoAggregate contrato, NumeroParcela numeroParcela);
        ValorMonetario CalcularJuros(ContratoDeEmprestimoAggregate contrato, NumeroParcela numeroParcela);
        decimal CalcularPrincipal(ContratoDeEmprestimoAggregate contrato, NumeroParcela numeroParcela);
    }
}