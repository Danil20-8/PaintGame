using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Networking;

namespace PaintGame.Core.Exceptions
{
    public class ServerOnlyException : Exception
    {
        public ServerOnlyException(NetworkBehaviour networkBehaviour)
            :base(string.Format("Called method of {0} is server only executing"))
        {
        }
    }
}
