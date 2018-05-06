using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SpecialInput
{
    public class DeviceConfigBase : IDeviceConfig
    {
        public T GetOption<T>(string name)
        {
            var type = GetType();
            var f = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (f != null)
            {
                if (f.FieldType == typeof(T))
                {
                    return (T)f.GetValue(this);
                }
                else
                    throw new DeviceConfigOptionTypeIsNotEqualException(name, typeof(T), f.FieldType);
            }
            else
                throw new DeviceConfigOptionIsNotExistException(name);
        }

        public void SetOption<T>(string name, T value)
        {
            var type = GetType();
            var f = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if(f != null)
            {
                if (f.FieldType == typeof(T))
                {
                    f.SetValue(this, value);
                }
                else
                    throw new DeviceConfigOptionTypeIsNotEqualException(name, typeof(T), f.FieldType);
            }
            else
                throw new DeviceConfigOptionIsNotExistException(name);
        }
    }

    public class DeviceConfigOptionIsNotExistException : Exception
    {
        public DeviceConfigOptionIsNotExistException(string name)
            :base("Option name \"" + name + "\" is not found")
        {
        }
    }
    public class DeviceConfigOptionTypeIsNotEqualException : Exception
    {
        public DeviceConfigOptionTypeIsNotEqualException(string name, Type dest, Type real)
            : base("Option name \"" + name + "\" type is not equal. " + dest.Name + " is awaited but option is " + dest.Name)
        {
        }
    }
}
