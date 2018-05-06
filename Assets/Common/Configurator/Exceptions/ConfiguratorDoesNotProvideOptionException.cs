using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurator
{
    public class ConfiguratorDoesNotProvideOptionException : Exception
    {
        public ConfiguratorDoesNotProvideOptionException(Type configuratorType, Type optionType)
            :base(string.Format("Configurator: {0}; Option: {1}", configuratorType.Name, optionType.Name))
        {
        }
    }
}
