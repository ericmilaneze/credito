using Credito.Domain.ValueObjects;

namespace Credito.Domain.ContratoDeEmprestimo
{
    public record Parcela
    {
        public NumeroParcela Numero { get; init; }
        public ValorMonetario SaldoDevedorInicial { get; init; }
        public ValorMonetario Valor { get; init; }
        public ValorMonetario Principal { get; init; }
        public ValorMonetario Juros { get; init; }

        public ValorMonetario SaldoDevedorFinal =>
            SaldoDevedorInicial - Principal;
    }
}