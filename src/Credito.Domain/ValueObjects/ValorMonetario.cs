using System;
using System.Collections.Generic;
using System.Linq;

namespace Credito.Domain.ValueObjects
{
    public record ValorMonetario : IComparable<ValorMonetario>
    {
        protected const string SIMBOLO_MOEDA = "R$";

        protected readonly decimal _valor;

        internal ValorMonetario(decimal valor) =>
            _valor = valor;

        public decimal ToDecimal() =>
            _valor;

        public override string ToString() =>
            $"{SIMBOLO_MOEDA} {_valor:#,###.00}";

        public int CompareTo(ValorMonetario other) =>
            _valor.CompareTo(other._valor);

        public static ValorMonetario FromDecimal(decimal valor) =>
            new ValorMonetario(valor);

        public static implicit operator ValorMonetario(decimal valor) => 
            FromDecimal(valor);
    }
}