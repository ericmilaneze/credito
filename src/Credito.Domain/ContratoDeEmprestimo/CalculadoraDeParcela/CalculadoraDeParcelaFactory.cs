using Credito.Domain.Common.ValueObjects;

namespace Credito.Domain.ContratoDeEmprestimo.CalculadoraDeParcela
{
    public class CalculadoraDeParcelaFactory
    {
        public static ICalculadoraDeParcelaStrategy Create(NumeroParcela numeroParcela) =>
            numeroParcela.IsFirst
                ? new CalculadoraDePrimeiraParcela()
                : new CalculadoraDeProximaParcela();
    }
}