using System;

namespace Credito.Domain.ValueObjects
{
    public record ValueFromDecimal : IComparable<ValueFromDecimal>
    {
        protected decimal _valor;

        internal ValueFromDecimal(decimal valor) =>
            _valor = valor;

        public decimal ToDecimal() =>
            _valor;

        public override string ToString() =>
            _valor.ToString();

        public int CompareTo(ValueFromDecimal other) =>
            _valor.CompareTo(other._valor);

        public static ValueFromDecimal FromDecimal(decimal valor) =>
            new ValueFromDecimal(valor);

        public static implicit operator ValueFromDecimal(decimal valor) => 
            FromDecimal(valor);
    }
}