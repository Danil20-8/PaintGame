using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpecialInput;
using PaintGame.Core.PGInput;
using ServiceLocator;
using PaintGame.Core.Config;
using UnityEngine;

namespace PaintGame.Core.ServiceFactories
{
    public class InputDeviceFactory
    {
        public IPlayerInput GetPlayerInput()
        {
            return new PGMKInputDevice(new PlayerControlConfigOptions().mkConfig);
        }
    }
}
