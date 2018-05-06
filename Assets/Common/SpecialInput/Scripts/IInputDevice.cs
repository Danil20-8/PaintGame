using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SpecialInput
{
    public interface IInputDevice
    {
        void RegisterForEvent(string name, Action action);
        void RegisterForAxis(string name, Action<float> action);
        void RegisterForStick(string name, Action<Vector2> action);
        void RegisterForEnd(Action action);

        void UnRegisterForEvent(string name, Action action);
        void UnRegisterForAxis(string name, Action<float> action);
        void UnRegisterForStick(string name, Action<Vector2> action);
        void UnRegisterForEnd(Action action);

        IDeviceConfig GetConfig();
        void SetConfig(IDeviceConfig config);
    }
}
