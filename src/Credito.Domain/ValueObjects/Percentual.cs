using System;

namespace Credito.Domain.ValueObjects
{
    public record Percentual : IComparable<Percentual>
    {
        protected decimal _valor;

        internal Percentual(decimal valor) =>
            _valor = valor;

        public decimal Aplicar(decimal valorTodo) =>
            valorTodo * (_valor / 100);

        public decimal ToDecimal() =>
            _valor;

        public override string ToString() =>
            $"{_valor:#,###.00}%";

        public int CompareTo(Percentual other) =>
            _valor.CompareTo(other._valor);

        public static Percentual FromDecimal(decimal valor) =>
            new Percentual(valor);

        public static implicit operator Percentual(decimal valor) => 
            FromDecimal(valor);
    }
}