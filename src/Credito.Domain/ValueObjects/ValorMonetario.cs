using System;
using System.Collections.Generic;
using System.Linq;

namespace Credito.Domain.ValueObjects
{
    public record ValorMonetario : ValueFromDecimal
    {
        protected const string SIMBOLO_MOEDA = "R$";

        internal ValorMonetario(decimal valor) : base(valor)
        {
            
        }

        public override string ToString() =>
            $"{SIMBOLO_MOEDA} {_valor:#,###.00}";

        public static ValorMonetario FromDecimal(decimal valor) =>
            new ValorMonetario(valor);

        public static implicit operator ValorMonetario(decimal valor) => 
            FromDecimal(valor);

        public static implicit operator ValorMonetario(double valor) => 
            FromDecimal((decimal)valor);
    }
}