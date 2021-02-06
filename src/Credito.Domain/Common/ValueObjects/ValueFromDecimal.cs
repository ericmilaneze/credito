using System;

namespace Credito.Domain.Common.ValueObjects
{
    public abstract record ValueFromDecimal : IComparable<ValueFromDecimal>
    {
        protected decimal _valor;

        internal ValueFromDecimal(decimal valor) =>
            _valor = valor;

        public static decimal ToDecimal(ValueFromDecimal value) =>
            value?._valor ?? default(decimal);

        public static decimal? ToDecimalNullable(ValueFromDecimal value) =>
            value?._valor;

        public static double ToDouble(ValueFromDecimal value) =>
            Convert.ToDouble(value?._valor ?? default(decimal));

        public override string ToString() =>
            _valor.ToString();

        public int CompareTo(ValueFromDecimal other) =>
            _valor.CompareTo(ValueFromDecimal.ToDecimal(other));

        public static decimal operator +(ValueFromDecimal v1, ValueFromDecimal v2) =>
            ToDecimal(v1) + ToDecimal(v2);

        public static decimal operator -(ValueFromDecimal v1, ValueFromDecimal v2) =>
            ToDecimal(v1) - ToDecimal(v2);

        public static decimal operator *(ValueFromDecimal v1, ValueFromDecimal v2) =>
            ToDecimal(v1) * ToDecimal(v2);

        public static decimal operator +(decimal v1, ValueFromDecimal v2) =>
            v1 + ToDecimal(v2);

        public static decimal operator *(decimal v1, ValueFromDecimal v2) =>
            v1 * ToDecimal(v2);
    }
}