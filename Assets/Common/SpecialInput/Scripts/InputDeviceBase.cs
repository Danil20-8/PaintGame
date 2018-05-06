using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SpecialInput
{
    public abstract class InputDeviceBase : MonoBehaviour, IInputDevice
    {
        public void RegisterForEvent(string name, Action action)
        {
            events.Find(e => e.name == name).AddListener(action);
        }
        public void RegisterForAxis(string name, Action<float> action)
        {
            axes.Find(a => a.name == name).AddListener(action);
        }
        public void RegisterForEnd(Action action)
        {
            endWaiters.Add(action);
        }

        public void RegisterForStick(string name, Action<Vector2> action)
        {
            sticks.Find(s => s.name == name).AddListener(action);
        }

        public void UnRegisterForEvent(string name, Action action)
        {
            events.Find(e => e.name == name).RemoveListener(action);
        }

        public void UnRegisterForAxis(string name, Action<float> action)
        {
            axes.Find(a => a.name == name).RemoveListener(action);
        }

        public void UnRegisterForEnd(Action action)
        {
            endWaiters.Remove(action);
        }

        public void UnRegisterForStick(string name, Action<Vector2> action)
        {
            sticks.Find(s => s.name == name).RemoveListener(action);
        }

        List<InputEvent> events;
        List<InputAxis> axes;
        List<InputStick> sticks;

        List<Action> endWaiters = new List<Action>();

        protected void Awake()
        {
            events = InitEvents().ToList();
            axes = InitAxes().ToList();
            sticks = InitSticks().ToList();
        }

        protected void Update()
        {
            foreach (var e in events)
                e.Process();
            foreach (var a in axes)
                a.Process();
            foreach (var s in sticks)
                s.Process();
            foreach (var e in endWaiters)
                e();
        }

        protected virtual IEnumerable<InputEvent> InitEvents()
        {
            return new InputEvent[0];
        }

        protected virtual IEnumerable<InputAxis> InitAxes()
        {
            return new InputAxis[0];
        }

        protected virtual IEnumerable<InputStick> InitSticks()
        {
            return new InputStick[0];
        }

        public abstract IDeviceConfig GetConfig();
        public abstract void SetConfig(IDeviceConfig config);
    }
}
