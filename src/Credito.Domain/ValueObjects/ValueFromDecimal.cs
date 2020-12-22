using System;

namespace Credito.Domain.ValueObjects
{
    public abstract record ValueFromDecimal : IComparable<ValueFromDecimal>
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
    }
}