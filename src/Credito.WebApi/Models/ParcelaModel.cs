namespace Credito.WebApi.Models
{
    public record ParcelaModel
    {
        public int Numero { get; init; }
        public decimal SaldoDevedorInicial { get; init; }
        public decimal Valor { get; init; }
        public decimal Principal { get; init; }
        public decimal Juros { get; init; }
        public decimal SaldoDevedorFinal { get; init; }
    }
}