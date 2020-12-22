using System;

namespace Credito.Domain.ValueObjects
{
    public record PrazoEmDias : ValueFromInt
    {
        internal PrazoEmDias(int valor) : base(valor)
        {
            
        }

        public override string ToString() =>
            $"{_valor} dias";

        public static PrazoEmDias FromInt(int valor)
        {
            if (valor <= 0)
                throw new ArgumentOutOfRangeException(nameof(valor), valor, "Prazo não pode ser menor ou igual a zero.");

            return new PrazoEmDias(valor);
        }

        public static implicit operator PrazoEmDias(int valor) => 
            FromInt(valor);
    }
}