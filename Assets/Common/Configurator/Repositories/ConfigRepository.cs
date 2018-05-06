using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurator
{
    public class ConfigRepository : IConfigRepository
    {

        string path;

        public ConfigRepository(string path)
        {
            this.path = path;
        }

        public Dictionary<Type, IOption> Load()
        {
            return new Dictionary<Type, IOption>();
        }

        public void Save(Dictionary<Type, IOption> options)
        {
        }
    }
}
