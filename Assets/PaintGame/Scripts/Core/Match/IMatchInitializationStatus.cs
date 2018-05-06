using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaintGame.Core.Match
{
    public interface IMatchInitializationStatus
    {
        void Reset();
        bool Check();

        void WaitMe(object service);

        void Ready(object service);
    }
}
