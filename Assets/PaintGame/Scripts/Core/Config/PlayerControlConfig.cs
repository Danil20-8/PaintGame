using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Configurator;
using PaintGame.Core.PGInput;

namespace PaintGame.Core.Config
{
    public class PlayerControlConfig : IOption<PlayerControlConfigOptions>
    {
        PlayerControlConfigOptions config;

        public PlayerControlConfigOptions GetValue()
        {
            return config;
        }

        public void SetValue(PlayerControlConfigOptions value)
        {
            config = value;
        }
    }

    public class PlayerControlConfigOptions
    {
        public PGMKInputConfig mkConfig;

        public PlayerControlConfigOptions()
        {
            mkConfig = new PGMKInputConfig
            {
                left = KeyCode.A,
                right = KeyCode.D,
                forward = KeyCode.W,
                back = KeyCode.S,

                jump = KeyCode.Space,
            };
        }
    }
}

