using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialInput
{
    public interface IDeviceConfig
    {
        T GetOption<T>(string name);
        void SetOption<T>(string name, T value);
    }
}
