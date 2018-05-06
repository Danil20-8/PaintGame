using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Messenger
{
    public interface IMessenger
    {
        void Subscribe<T>(Action<T> listener, bool listenForDerived = false);
        void UnSubscribe<T>(Action<T> listener);
        void Push<T>(T message);
    }
}
