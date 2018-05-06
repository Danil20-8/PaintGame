using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Configurator
{
    public class InputProcessorOption : OptionBase<InputProcessorOptions>
    {
        public InputProcessorOption()
            :this(InputProcessorOptions.MouseKeyboard)
        {

        }

        public InputProcessorOption(InputProcessorOptions value)
            :base(value)
        {
        }
    }

    public enum InputProcessorOptions
    {
        MouseKeyboard,
        TouchScreen
    }
}
