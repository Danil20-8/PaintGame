using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PaintGame.Core.Spawn
{
    public delegate T PrefabSelector<T, TPrefabs>(TPrefabs prefabs) where T : Component;
    public delegate void GameObjectInitializer<T>(T gameObject) where T : Component;

    public interface ISpawnManager<TPrefabs>
    {
        T GetPrefab<T>(PrefabSelector<T, TPrefabs> selector)
            where T : Component;

        T SpawnPrefab<T>(PrefabSelector<T, TPrefabs> selector, Vector3 position, Quaternion rotation, GameObjectInitializer<T> initializer)
            where T : Component;

        T SpawnPrefab<T>(T prefab, Vector3 position, Quaternion rotation, GameObjectInitializer<T> initializer)
            where T : Component;

        T SpawnPlayerFor<T>(PrefabSelector<T, TPrefabs> selector, Vector3 position, Quaternion rotation, GameObjectInitializer<T> initializer, GameObject player)
            where T : Component;

        T SpawnPlayerFor<T>(T prefab, Vector3 position, Quaternion rotation, GameObjectInitializer<T> initializer, GameObject player)
            where T : Component;
    }
}
