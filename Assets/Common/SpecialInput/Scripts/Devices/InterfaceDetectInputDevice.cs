using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;
namespace SpecialInput.Devices
{
    public abstract class InterfaceDetectInputDevice<TInterface> : InputDeviceBase
        where TInterface : class
    {
        sealed protected override IEnumerable<InputAxis> InitAxes()
        {
            return GetInputMethods()
                .Where(m => m.ReturnType == typeof(float))
                .Select(m => new InputAxis(m.Name, Delegate.CreateDelegate(typeof(Func<float>), this as TInterface, m) as Func<float>))
                .ToArray();
        }
        sealed protected override IEnumerable<InputStick> InitSticks()
        {
            return GetInputMethods()
                .Where(m => m.ReturnType == typeof(Vector2))
                .Select(m => new InputStick(m.Name, Delegate.CreateDelegate(typeof(Func<Vector2>), this as TInterface, m) as Func<Vector2>))
                .ToArray();
        }
        sealed protected override IEnumerable<InputEvent> InitEvents()
        {
            return GetInputMethods()
                .Where(m => m.ReturnType == typeof(bool))
                .Select(m => new InputEvent(m.Name, Delegate.CreateDelegate(typeof(Func<bool>), this as TInterface, m) as Func<bool>))
                .ToArray();
        }

        IEnumerable<MethodInfo> GetInputMethods()
        {
            var type = GetType();
            return typeof(TInterface).GetInterfaces().SelectMany(i => type.GetInterfaceMap(i).TargetMethods).Concat(type.GetInterfaceMap(typeof(TInterface)).TargetMethods);
        }
    }
}
