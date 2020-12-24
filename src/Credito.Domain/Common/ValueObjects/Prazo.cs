using System;

namespace Credito.Domain.Common.ValueObjects
{
    public record Prazo : ValueFromInt
    {
        internal Prazo(int valor) : base(valor)
        {
            
        }

        public override string ToString() =>
            $"{_valor}";

        public static Prazo FromInt(int valor)
        {
            if (valor < 0)
                throw new ArgumentOutOfRangeException(nameof(valor), valor, "Prazo não pode ser menor que zero.");

            return new Prazo(valor);
        }

        public static implicit operator Prazo(int valor) => 
            FromInt(valor);
    }
}