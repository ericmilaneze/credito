using System;

namespace Credito.Domain.ValueObjects
{
    public record Percentual : ValueFromDecimal
    {
        internal Percentual(decimal valor) : base(valor)
        {

        }

        public decimal Aplicar(decimal valorTodo) =>
            valorTodo * (_valor / 100);

        public override string ToString() =>
            $"{_valor:#,###.00}%";

        public static Percentual FromDecimal(decimal valor) =>
            new Percentual(valor);

        public static implicit operator Percentual(decimal valor) => 
            FromDecimal(valor);
    }
}