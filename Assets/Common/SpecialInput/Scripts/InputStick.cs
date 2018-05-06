using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SpecialInput
{
    public class InputStick
    {
        public string name { get; set; }
        Func<Vector2> getStick;

        List<Action<Vector2>> listeners = new List<Action<Vector2>>();

        public InputStick(string name, Func<Vector2> getStick)
        {
            this.name = name;
            this.getStick = getStick;
        }

        public void AddListener(Action<Vector2> listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(Action<Vector2> listener)
        {
            listeners.Remove(listener);
        }

        public void Process()
        {
            var stick = getStick();
            foreach (var l in listeners)
                l(stick);
        }
    }
}
