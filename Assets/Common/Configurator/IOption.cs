using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace Configurator
{
    public interface IOption<TValue> : IOption
    {
        TValue GetValue();
        void SetValue(TValue value);
    }

    public interface IOption
    {
    }

    public static class OptionExtensions
    {
        static Dictionary<Type, MethodInfo> optionValueCache = new Dictionary<Type, MethodInfo>();
        static object[] emptyParameters = new object[0];

        public static object GetValue(this IOption option)
        {
            var optionType = option.GetType();

            MethodInfo getMethod;
            if(!optionValueCache.TryGetValue(optionType, out getMethod))
                getMethod = optionType.GetMethod("GetValue", BindingFlags.Instance | BindingFlags.Public);

            return getMethod.Invoke(option, emptyParameters);
        }
    }
}
