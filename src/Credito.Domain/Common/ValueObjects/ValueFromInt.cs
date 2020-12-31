using System;

namespace Credito.Domain.Common.ValueObjects
{
    public abstract record ValueFromInt : IComparable<ValueFromInt>
    {
        protected int _valor;

        internal ValueFromInt(int valor) =>
            _valor = valor;

        public static int ToInt(ValueFromInt value) =>
            value?._valor ?? default(int);

        public static int? ToIntNullable(ValueFromInt value) =>
            value?._valor;

        public static decimal ToDecimal(ValueFromInt value) =>
            ToInt(value);

        public override string ToString() =>
            _valor.ToString();

        public int CompareTo(ValueFromInt other) =>
            _valor.CompareTo(other?._valor);

        public static int operator -(ValueFromInt v1, ValueFromInt v2) =>
            ToInt(v1) - ToInt(v2);

        public static int operator -(ValueFromInt v1, int v2) =>
            ToInt(v1) - v2;
    }
}