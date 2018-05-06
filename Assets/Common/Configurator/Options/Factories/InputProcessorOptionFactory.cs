using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurator
{
    public class InputProcessorOptionFactory : IOptionFactory
    {
        public IOption GetOption()
        {
            return new InputProcessorOption();
        }
    }
}
