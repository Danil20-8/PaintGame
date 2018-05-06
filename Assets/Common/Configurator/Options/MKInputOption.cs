using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecialInput.Devices;

namespace Configurator
{
    public class MKInputOption : OptionBase<MKInputProcessorConfiguration>
    {
        public MKInputOption(MKInputProcessorConfiguration mkInputProcessorConfiguration)
            :base(mkInputProcessorConfiguration)
        {
        }
    }

    public class MKInputProcessorConfiguration
    {
        List<MKInputConfig> configs;

        public MKInputProcessorConfiguration(IEnumerable<MKInputConfig> configs)
        {
            this.configs = configs.ToList();
        }

        public MKInputConfig GetConfig(int deviceId)
        {
            return configs[deviceId];
        }
    }
}
