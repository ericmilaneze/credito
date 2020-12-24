using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Credito.Domain.Common.ValueObjects
{
    public static class ValueFromIntExtensions
    {
        public static T Sum<T>(this IEnumerable<T> source) where T : ValueFromInt
        {
            int sum = 0;
            checked {
                foreach (int v in source?.Select(v => v?.ToInt() ?? default(int)) ?? Enumerable.Empty<int>())
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