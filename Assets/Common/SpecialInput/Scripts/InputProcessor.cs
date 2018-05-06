using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SpecialInput
{
    public class InputProcessor : MonoBehaviour, ISpecialInput
    {
        public IInputDevice[] inputs;

        public void AddProfile(InputProfile profile, int device = 0)
        {
            var input = inputs[device];
            foreach (var e in profile.events)
                input.RegisterForEvent(e.name, e.action);
            foreach (var a in profile.axes)
                input.RegisterForAxis(a.name, a.action);
            foreach (var s in profile.sticks)
                input.RegisterForStick(s.name, s.action);

            if (profile.endInputListener != null)
                input.RegisterForEnd(profile.endInputListener);
        }

        public void RemoveProfile(InputProfile profile, int device = 0)
        {
            var input = inputs[device];
            foreach (var e in profile.events)
                input.UnRegisterForEvent(e.name, e.action);
            foreach (var a in profile.axes)
                input.UnRegisterForAxis(a.name, a.action);
            foreach (var s in profile.sticks)
                input.UnRegisterForStick(s.name, s.action);

            if (profile.endInputListener != null)
                input.UnRegisterForEnd(profile.endInputListener);
        }
    }
}
