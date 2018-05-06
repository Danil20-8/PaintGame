using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialInput
{
    public class InputAxis
    {
        public string name { get; private set; }
        Func<float> getAxis;

        List<Action<float>> listeners = new List<Action<float>>();

        public InputAxis(string name, Func<float> getAxis)
        {
            this.name = name;
            this.getAxis = getAxis;
        }

        public void AddListener(Action<float> listener)
        {
            listeners.Add(listener);
        }

        public void RemoveListener(Action<float> listener)
        {
            listeners.Remove(listener);
        }

        public void Process()
        {
            var axis = getAxis();
            foreach (var l in listeners)
                l(axis);
        }
    }
}
