using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PaintGame.Core.Spawn
{
    public class NullPrefabException : Exception
    {
        public NullPrefabException(object holder):
            base(string.Format("{0} returned null prefab reference.", holder))
        {
        }
    }
}
