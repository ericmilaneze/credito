using System;

namespace Credito.Domain.ValueObjects
{
    public record ValorMonetarioPositivo : ValorMonetario
    {
        internal ValorMonetarioPositivo(decimal valor) : base(valor) 
        {

        }

        public override string ToString() =>
            base.ToString();

        public new static ValorMonetarioPositivo FromDecimal(decimal valor)
        {
            if (valor <= 0.00M)
                throw new ArgumentOutOfRangeException(nameof(valor), valor, "Valor não pode ser menor ou igual a zero.");

            return new ValorMonetarioPositivo(valor);
        }

        public static implicit operator ValorMonetarioPositivo(decimal valor) => 
            FromDecimal(valor);
    }
}