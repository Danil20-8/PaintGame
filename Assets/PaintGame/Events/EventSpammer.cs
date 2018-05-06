using System.Collections.Generic;
using UnityEngine;

namespace PaintGame.Events
{
    public static class EventSpammer
    {
        static Stack<List<EventListenerBehaviour>> lists = new Stack<List<EventListenerBehaviour>>();

        public static void SpamDown<TEvent>(GameObject gameObject, TEvent e)
        {
            var list = GetList();
            gameObject.GetComponentsInChildren(false, list);

            foreach(var listener in list)
                listener.Raise(e);

            lists.Push(list);
        }

        public static void SpamUp<TEvent>(GameObject gameObject, TEvent e)
        {
            var list = GetList();
            gameObject.GetComponentsInParent(false, list);

            foreach (var listener in list)
                listener.Raise(e);

            lists.Push(list);
        }

        static List<EventListenerBehaviour> GetList()
        {
            if (lists.Count == 0)
                return new List<EventListenerBehaviour>();
            else
            {
                var list = lists.Pop();
                list.Clear();
                return list;
            }
        }
    }
}
