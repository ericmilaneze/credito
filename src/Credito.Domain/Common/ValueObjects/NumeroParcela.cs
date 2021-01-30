using System;

namespace Credito.Domain.Common.ValueObjects
{
    public record NumeroParcela : ValueFromInt
    {
        internal NumeroParcela(int valor) : base(valor)
        { }

        public bool IsFirst =>
            _valor == 1;

        public override string ToString() =>
            $"Parcela #{_valor}";

        public static NumeroParcela FromInt(int valor)
        {
            if (valor <= 0)
                throw new ArgumentOutOfRangeException(nameof(valor), valor, "Número da parcela não pode ser menor ou igual a zero.");

            return new NumeroParcela(valor);
        }

        public static implicit operator NumeroParcela(int valor) => 
            FromInt(valor);

        public static implicit operator NumeroParcela(long valor) => 
            FromInt((int)valor);
    }
}