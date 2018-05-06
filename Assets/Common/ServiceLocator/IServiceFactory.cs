using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLocator
{
    public interface IServiceFactory<TService>
    {
        TService GetService();
    }
}
