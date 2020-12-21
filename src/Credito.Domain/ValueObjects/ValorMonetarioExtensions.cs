using System.Collections.Generic;
using System.Linq;

namespace Credito.Domain.ValueObjects
{
    public static class ValorMonetarioExtensions
    {
        public static ValorMonetario Sum(this IEnumerable<ValorMonetario> valores)
        {
            decimal sum = 0M;
            checked {
                foreach (decimal v in valores?.Select(v => v?.ToDecimal() ?? default(decimal)) ?? Enumerable.Empty<decimal>())
                    sum += v;
            }
            return sum;
        }

        public static ValorMonetarioPositivo Sum(this IEnumerable<ValorMonetarioPositivo> valores)
        {
            var ret = valores?.Select(v => new ValorMonetario(v?.ToDecimal() ?? default(decimal)))?.Sum() ?? default(decimal);
            return new ValorMonetarioPositivo(ret.ToDecimal());
        }
    }
}