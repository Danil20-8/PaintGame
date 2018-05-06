using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurator
{
    public interface IConfigRepository
    {
        Dictionary<Type, IOption> Load();
        void Save(Dictionary<Type, IOption> options);
    }
}
