using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class DictionaryHelpers
{
    public static TValue GetValueOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        where TValue : class
    {
        TValue result;
        dict.TryGetValue(key, out result);
        return result;
    }
}
