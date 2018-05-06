using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpecialInput
{
    public class InputEvent
    {
        public string name { get; private set; }
        Func<bool> checker;
        List<Action> listeners = new List<Action>();

        public InputEvent(string name, Func<bool> inputChecker)
        {
            this.name = name;
            this.checker = inputChecker;
        }

        public void AddListener(Action listener)
        {
            listeners.Add(listener);
        }
        public void RemoveListener(Action listener)
        {
            listeners.Remove(listener);
        }

        public void Process()
        {
            if (checker())
                foreach (var l in listeners)
                    l();
        }
    }
}
