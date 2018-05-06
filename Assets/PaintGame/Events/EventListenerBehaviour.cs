using System;
using System.Collections.Generic;
using UnityEngine;

namespace PaintGame.Events
{
    public class EventListenerBehaviour : MonoBehaviour
    {
        Dictionary<Type, List<IEventListener>> listeners = new Dictionary<Type, List<IEventListener>>();

        public void Raise<TEvent>(TEvent e)
        {
            List<IEventListener> eListeners;
            if(listeners.TryGetValue(typeof(TEvent), out eListeners))
            {
                foreach(EventListener<TEvent> listener in eListeners)
                    listener.Invoke(e);
            }
        }

        public void Listen<TEvent>(Action<TEvent> listener)
        {
            List<IEventListener> eListeners;
            if(!listeners.TryGetValue(typeof(TEvent), out eListeners))
            {
                listeners[typeof(TEvent)] = new List<IEventListener>() { new EventListener<TEvent>(listener) };
            }
            else
            {
                eListeners.Add(new EventListener<TEvent>(listener));
            }
        }

        public void StopListen<TEvent>(Action<TEvent> listener)
        {
            List<IEventListener> eListeners;
            if (listeners.TryGetValue(typeof(TEvent), out eListeners))
            {
                for(var index = 0; index < eListeners.Count; ++index)
                {
                    if((eListeners[index] as EventListener<TEvent>).IsThis(listener))
                    {
                        eListeners.RemoveAt(index);
                        return;
                    }
                }
            }
        }

        interface IEventListener
        {
        }

        class EventListener<TEvent> : IEventListener
        {
            private readonly Action<TEvent> listener;

            public EventListener(Action<TEvent> listener)
            {
                this.listener = listener;
            }

            public void Invoke(TEvent e)
            {
                listener(e);
            }

            public bool IsThis(Action<TEvent> listener)
            {
                return this.listener == listener;
            }
        }
    }
}
