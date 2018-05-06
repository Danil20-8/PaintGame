using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;

namespace SpecialInput.Devices
{
    public class MKInput : InterfaceDetectInputDevice<ICharacterInput>, ICharacterInput
    {
        MKInputConfig config = new MKInputConfig();

        KeyCode left { get { return config.left; } }
        KeyCode right { get { return config.right; } }
        KeyCode forward { get { return config.forward; } }
        KeyCode back { get { return config.back; } }

        KeyCode turnLeft { get { return config.turnLeft; } }
        KeyCode turnRight { get { return config.turnRight; } }

        KeyCode jump { get { return config.jump; } }
        KeyCode fire { get { return config.fire; } }

        public float Horizontal()
        {
            return Input.GetKey(left) ? -1f : (Input.GetKey(right) ? 1f : 0f);
        }
        public float Vertical()
        {
            return Input.GetKey(back) ? -1f : (Input.GetKey(forward) ? 1f : 0f);
        }
        public bool Jump()
        {
            return Input.GetKeyDown(jump);
        }

        public Vector2 MoveStick()
        {
            return new Vector2(Horizontal(), Vertical());
        }

        public Vector2 ViewStick()
        {
            return new Vector2(Input.GetKey(turnLeft) ? -1f : (Input.GetKey(turnRight) ? 1f : 0f), 0f);
        }


        public override IDeviceConfig GetConfig()
        {
            return config;
        }

        public bool Fire()
        {
            return Input.GetKey(fire);
        }

        public override void SetConfig(IDeviceConfig config)
        {
            var mkConfig = config as MKInputConfig;
            if (mkConfig != null)
                this.config = mkConfig;
        }
    }

    [Serializable]
    public class MKInputConfig : DeviceConfigBase
    {
        public KeyCode left = KeyCode.A;
        public KeyCode right = KeyCode.D;
        public KeyCode forward = KeyCode.W;
        public KeyCode back = KeyCode.S;

        public KeyCode turnLeft = KeyCode.Q;
        public KeyCode turnRight = KeyCode.E;

        public KeyCode jump = KeyCode.Space;

        public KeyCode fire = KeyCode.LeftControl;
    }
}
