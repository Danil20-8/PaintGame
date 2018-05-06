using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Messenger
{
    public class Messenger : MonoBehaviour, IMessenger
    {
        DRLib.Patterns.Observer.Messenger messenger = new DRLib.Patterns.Observer.Messenger();

        public void Push<T>(T message)
        {
            messenger.Send(message);
        }

        public void Subscribe<T>(Action<T> listener, bool listenForDerived = false)
        {
            messenger.AddListener(listener, listenForDerived);
        }

        public void UnSubscribe<T>(Action<T> listener)
        {
            messenger.RemoveListener(listener);
        }
    }
}
