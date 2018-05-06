using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurator
{
    public class FactoryCannotCreateOptionException : Exception
    {
        public FactoryCannotCreateOptionException(Type factoryType, Type optionType)
            : base(string.Format("Factory: {0}; Option: {1};", factoryType.Name, optionType.Name))
        {

        }
    }
}
