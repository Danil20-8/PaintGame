﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Core.Spawn
{
    public interface ISpawnSerializable
    {
        IEnumerable<NetworkIdentity> Serialize();
    }
}
