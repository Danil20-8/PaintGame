using System;

namespace PaintGame.Events
{
    public static class GlobalEventListener
    {
        static GlobalEventListenerBehaviour listener;

        public static void SetListener(GlobalEventListenerBehaviour listener)
        {
            GlobalEventListener.listener = listener;
        }

        public static void Raise<TEvent>(TEvent e)
        {
            listener.Raise(e);
        }

        public static void Listen<TEvent>(Action<TEvent> listener)
        {
            GlobalEventListener.listener.Listen(listener);
        }

        public static void StopListen<TEvent>(Action<TEvent> listener)
        {
            GlobalEventListener.listener.StopListen(listener);
        }
    }
}
