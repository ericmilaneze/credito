using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Credito.Domain.Common.ValueObjects
{
    public static class ValueFromDecimalExtensions
    {
        public static T Sum<T>(this IEnumerable<T> source) where T : ValueFromDecimal
        {
            decimal sum = 0M;
            checked {
                foreach (decimal v in source?.Select(v => ValueFromDecimal.ToDecimal(v)) ?? Enumerable.Empty<decimal>())
                    sum += v;
            }
            return (T)Activator.CreateInstance(typeof(T),
                                               BindingFlags.Instance | BindingFlags.NonPublic,
                                               null,
                                               new object[] { sum },
                                               null);
        }
    }
}