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

        public override string ToString() =>
            _valor.ToString();

        public int CompareTo(ValueFromInt other) =>
            _valor.CompareTo(other._valor);
    }
}