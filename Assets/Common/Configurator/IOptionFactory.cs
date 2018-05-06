using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurator
{
    public interface ICommonOptionFactory
    {
        TOption GetOption<TOption>() where TOption : class, IOption;
    }
    public interface IOptionFactory
    {
        IOption GetOption();
    }
}
