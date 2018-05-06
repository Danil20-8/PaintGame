using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurator
{
    public class DefaultOptionFactory : ICommonOptionFactory
    {
        TOption ICommonOptionFactory.GetOption<TOption>()
        {
            return Activator.CreateInstance<TOption>();
        }
    }
}
