using UnityEngine;

namespace PaintGame.Events
{
    public class GlobalEventListenerBehaviour : EventListenerBehaviour
    {
        static GlobalEventListenerBehaviour listener;

        protected void Awake()
        {
            if (listener != null)
                Destroy(gameObject);
            listener = this;
            GlobalEventListener.SetListener(this);
        }

        protected void OnDestroy()
        {
            listener = null;
        }
    }
}
