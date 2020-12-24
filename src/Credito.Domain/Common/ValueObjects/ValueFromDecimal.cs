using System;

namespace Credito.Domain.Common.ValueObjects
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

        public static decimal operator +(ValueFromDecimal v1, ValueFromDecimal v2) =>
            v1._valor + v2._valor;

        public static decimal operator -(ValueFromDecimal v1, ValueFromDecimal v2) =>
            v1._valor - v2._valor;

        public static decimal operator *(ValueFromDecimal v1, ValueFromDecimal v2) =>
            v1._valor * v2._valor;

        public static decimal operator +(decimal v1, ValueFromDecimal v2) =>
            v1 + v2._valor;

        public static decimal operator *(decimal v1, ValueFromDecimal v2) =>
            v1 * v2._valor;
    }
}