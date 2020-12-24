using System;

namespace Credito.Domain.Common.ValueObjects
{
    public record PercentualPositivo : Percentual
    {
        internal PercentualPositivo(decimal valor) : base(valor)
        {

        }

        public override string ToString() =>
            base.ToString();

        public new static PercentualPositivo FromDecimal(decimal valor)
        {
            if (valor <= 0.00M)
                throw new ArgumentOutOfRangeException(nameof(valor), valor, "Percentual não pode ser menor ou igual a zero.");

            return new PercentualPositivo(valor);
        }

        public static implicit operator PercentualPositivo(decimal valor) => 
            FromDecimal(valor);
    }
}