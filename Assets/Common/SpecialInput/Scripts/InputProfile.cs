using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Reflection;

namespace SpecialInput
{
    public class InputProfile
    {
        public InputProfile(Action endInputListener = null)
        {
            this.endInputListener = endInputListener;
        }

        public void RegisterForEvent<InputInterface>(Action action)
        {
            _events.Add(new EventListener(GetInputName<InputInterface>(), action));
        }
        public void RegisterForEvent(string name, Action action)
        {
            _events.Add(new EventListener(name, action));
        }
        public void RegisterForAxis<InputInterface>(Action<float> action) where InputInterface : class
        {
            RegisterForAxis(GetInputName<InputInterface>(), action);
        }
        public void RegisterForAxis(string name, Action<float> action)
        {
            _axes.Add(new AxisListener(name, action));
        }
        public void RegisterForSticks<InputInterface>(Action<Vector2> action) where InputInterface : class
        {
            RegisterForSticks(GetInputName<InputInterface>(), action);
        }
        public void RegisterForSticks(string name, Action<Vector2> action)
        {
            _sticks.Add(new StickListener(name, action));
        }

        string GetInputName<InputInterface>()
        {
            return typeof(InputInterface).GetMethods(BindingFlags.Instance | BindingFlags.Public).Single().Name;
        }

        public Action endInputListener { get; private set; }

        List<EventListener> _events = new List<EventListener>();
        List<AxisListener> _axes = new List<AxisListener>();
        List<StickListener> _sticks = new List<StickListener>();

        public IEnumerable<EventListener> events { get { return _events; } }
        public IEnumerable<AxisListener> axes { get { return _axes; } }
        public IEnumerable<StickListener> sticks { get { return _sticks; } }
    }

    public class EventListener
    {
        public string name { get; private set; }
        public Action action { get; private set; }

        public EventListener(string name, Action action)
        {
            this.name = name;
            this.action = action;
        }
    }

    public class AxisListener
    {
        public string name { get; private set; }
        public Action<float> action { get; private set; }

        public AxisListener(string name, Action<float> action)
        {
            this.name = name;
            this.action = action;
        }
    }

    public class StickListener
    {
        public string name { get; private set; }
        public Action<Vector2> action { get; private set; }

        public StickListener(string name, Action<Vector2> action)
        {
            this.name = name;
            this.action = action;
        }
    }
}
