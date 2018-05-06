using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class EnumerableHelper
{
    public static string ToString<T>(this IEnumerable<T> enumerable, string separator)
    {
        return string.Join(separator, enumerable.Select(e => e.ToString()).ToArray());
    }
}

