using System;

namespace Credito.Domain.ValueObjects
{
    public abstract record ValueFromInt : IComparable<ValueFromInt>
    {
        protected int _valor;

        internal ValueFromInt(int valor) =>
            _valor = valor;

        public int ToInt() =>
            _valor;

        public decimal ToDecimal() =>
            _valor;

        public override string ToString() =>
            _valor.ToString();

        public int CompareTo(ValueFromInt other) =>
            _valor.CompareTo(other._valor);

        public static int operator -(ValueFromInt v1, ValueFromInt v2) =>
            v1._valor - v2._valor;

        public static int operator -(ValueFromInt v1, int v2) =>
            v1._valor - v2;
    }
}