using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurator
{
    public class OptionBase<TValue> : IOption<TValue>
    {
        TValue value;

        public OptionBase(TValue value)
        {
            this.value = value;
        }

        public TValue GetValue()
        {
            return value;
        }

        public void SetValue(TValue value)
        {
            this.value = value;
        }
    }
}
