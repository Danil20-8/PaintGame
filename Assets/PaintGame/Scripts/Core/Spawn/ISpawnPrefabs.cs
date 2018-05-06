using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Reflection;
namespace PaintGame.Core.Spawn
{
    public interface ISpawnPrefabs
    {
    }

    public static class SpawnPrefabsExtensions
    {
        public static IEnumerable<NetworkIdentity> Serialize(this ISpawnPrefabs prefabs)
        {
            return new SpawnPrefabsSerializer().Serialize(prefabs);
        }
    }
}
