using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurator
{
    public interface IConfigurator
    {
        TOption GetOption<TOption>()
            where TOption :  class, IOption
            ;

        void Save();
    }
}
