using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Core.Spawn
{
    public class SpawnManager : MonoBehaviour, ISpawnManager<SpawnPrefabs>
    {
        [SerializeField]
        SpawnPrefabs prefabs;

        uint networkInstanceId = 0;

        void Start()
        {
            //NetworkServer.objects.Clear();
            foreach (var p in prefabs.Serialize())
            {
                NetworkLobbyManager.singleton.spawnPrefabs.Add(p.gameObject);
                //NetworkServer.objects.Add(new NetworkInstanceId(networkInstanceId++), p);
            }

        }

        public T GetPrefab<T>(PrefabSelector<T, SpawnPrefabs> selector)
            where T : Component
        {
            return selector(prefabs);
        }

        public T SpawnPrefab<T>(PrefabSelector<T, SpawnPrefabs> selector, Vector3 position, Quaternion rotation, GameObjectInitializer<T> initializer)
            where T : Component
        {
            var prefab = selector(prefabs);
            T gameObject = GameObject.Instantiate(prefab, position, rotation);
            initializer(gameObject);
            NetworkServer.Spawn(gameObject.gameObject);
            return gameObject;
        }

        public T SpawnPlayerFor<T>(PrefabSelector<T, SpawnPrefabs> selector, Vector3 position, Quaternion rotation, GameObjectInitializer<T> initializer, GameObject player)
            where T : Component
        {
            var prefab = selector(prefabs);
            T gameObject = GameObject.Instantiate(prefab, position, rotation);
            initializer(gameObject);
            NetworkServer.SpawnWithClientAuthority(gameObject.gameObject, player);
            return gameObject;
        }

        public T SpawnPrefab<T>(T prefab, Vector3 position, Quaternion rotation, GameObjectInitializer<T> initializer) where T : Component
        {
            T gameObject = GameObject.Instantiate(prefab, position, rotation);
            initializer(gameObject);
            NetworkServer.Spawn(gameObject.gameObject);
            return gameObject;
        }

        public T SpawnPlayerFor<T>(T prefab, Vector3 position, Quaternion rotation, GameObjectInitializer<T> initializer, GameObject player) where T : Component
        {
            T gameObject = GameObject.Instantiate(prefab, position, rotation);
            initializer(gameObject);
            NetworkServer.SpawnWithClientAuthority(gameObject.gameObject, player);
            return gameObject;
        }
    }
}
