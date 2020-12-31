using System;

namespace Credito.Domain.Common.ValueObjects
{
    public record Percentual : ValueFromDecimal
    {
        internal Percentual(decimal valor) : base(valor)
        {

        }

        public decimal Aplicar(decimal valorTodo) =>
            valorTodo * ObterPercentual();

        public decimal Aplicar(int valorTodo) =>
            valorTodo * ObterPercentual();

        public ValorMonetario Aplicar(ValueFromDecimal valorTodo) =>
            ValueFromDecimal.ToDecimal(valorTodo) * ObterPercentual();

        public ValorMonetario Aplicar(ValorMonetario valorTodo) =>
            ValueFromDecimal.ToDecimal(valorTodo) * ObterPercentual();

        public decimal Aplicar(ValueFromInt valorTodo) =>
            ValueFromInt.ToDecimal(valorTodo) * ObterPercentual();

        public decimal ObterPercentual() =>
            _valor / 100;

        public override string ToString() =>
            $"{_valor:#,###.00}%";

        public static Percentual FromDecimal(decimal valor) =>
            new Percentual(valor);

        public static Percentual FromDouble(double valor) =>
            new Percentual(Decimal.Parse(valor.ToString()));

        public static implicit operator Percentual(decimal valor) => 
            FromDecimal(valor);

        public static implicit operator Percentual(double valor) => 
            FromDouble(valor);

        public static decimal operator +(Percentual v1, decimal v2) =>
            v1.ObterPercentual() + v2;

        public static decimal operator +(decimal v1, Percentual v2) =>
            v2.ObterPercentual() + v1;

        public static decimal operator +(int v1, Percentual v2) =>
            v2.ObterPercentual() + v1;

        public static decimal operator *(Percentual v1, ValueFromDecimal v2) =>
            v1.Aplicar(ValueFromDecimal.ToDecimal(v2));

        public static decimal operator *(Percentual v1, ValueFromInt v2) =>
            v1.Aplicar(ValueFromInt.ToDecimal(v2));

        public static decimal operator *(Percentual v1, decimal v2) =>
            v1.Aplicar(v2);

        public static decimal operator /(Percentual v1, decimal v2) =>
            v1.ObterPercentual() / v2;

        public static decimal operator /(Percentual v1, double v2) =>
            v1.ObterPercentual() / Decimal.Parse(v2.ToString());
    }
}