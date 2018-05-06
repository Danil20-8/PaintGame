using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using SpecialInput.Devices;

namespace Configurator
{
    public class MKInputOptionFactory : IOptionFactory
    {
        public IOption GetOption()
        {
            var configs = new MKInputProcessorConfiguration(new MKInputConfig[]
                {
                    new MKInputConfig
                    {
                        left = KeyCode.A,
                        right = KeyCode.D,
                        forward = KeyCode.W,
                        back = KeyCode.S,

                        turnLeft = KeyCode.Q,
                        turnRight = KeyCode.E,

                        jump = KeyCode.Space
                    },
                    new MKInputConfig
                    {
                        back = KeyCode.Keypad2,
                        forward = KeyCode.Keypad8,
                        left = KeyCode.Keypad4,
                        right = KeyCode.Keypad6,

                        turnLeft = KeyCode.Keypad7,
                        turnRight = KeyCode.Keypad9,

                        jump = KeyCode.Keypad0
                    }
                });

            return new MKInputOption(configs);
        }
    }

    public enum PlayerDeviceId
    {
        Player1 = 0,
        Player2 = 1,
    }
}
