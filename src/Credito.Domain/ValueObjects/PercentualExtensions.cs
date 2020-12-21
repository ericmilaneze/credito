using System.Collections.Generic;
using System.Linq;

namespace Credito.Domain.ValueObjects
{
    public static class PercentualExtensions
    {
        public static Percentual Sum(this IEnumerable<Percentual> percentuais)
        {
            decimal sum = 0M;
            checked {
                foreach (decimal v in percentuais?.Select(v => v?.ToDecimal() ?? default(decimal)) ?? Enumerable.Empty<decimal>())
                    sum += v;
            }
            return sum;
        }
    }
}