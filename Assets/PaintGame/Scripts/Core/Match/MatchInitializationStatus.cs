using PaintGame.Core.Match.Messeges;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PaintGame.Core.Match
{
    public class MatchInitializationStatus : MonoBehaviour, IMatchInitializationStatus
    {
        List<object> services = new List<object>();
        bool initialized;

        public bool Check()
        {
            if(services.Count == 0)
            {
                initialized = true;
                PGServiceLocator.Messenger.Push(new MatchInitializedMessege());
                return true;
            }
            return false;
        }

        public void Ready(object service)
        {
            if(!services.Remove(service))
            {
                throw new InvalidOperationException(service + " is not registered");
            }
            else if(services.Count == 0)
            {
                initialized = true;
                PGServiceLocator.Messenger.Push(new MatchInitializedMessege());
            }
        }

        public void Reset()
        {
            if (services.Count > 0)
                throw new InvalidOperationException("Initialization is not completed");

            PGServiceLocator.Messenger.Push(new MatchInitializationStartedMessege());
            initialized = false;
        }

        public void WaitMe(object service)
        {
            if (initialized)
                throw new InvalidOperationException("Match is already initialized");
            services.Add(service);
        }
    }

    public static class MatchInitializationStartedMessegeExtensions
    {
        public static void WaitMe(this MatchInitializationStartedMessege m, object service)
        {
            PGServiceLocator.MatchInitializationStatus.WaitMe(service);
        }
    }
}
