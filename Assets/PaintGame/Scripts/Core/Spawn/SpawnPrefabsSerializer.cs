using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Core.Spawn
{
    public class SpawnPrefabsSerializer
    {
        private readonly Dictionary<Type, TypeInterfaces> interfaces = new Dictionary<Type, TypeInterfaces>();

        public IEnumerable<NetworkIdentity> Serialize(ISpawnPrefabs prefabs)
        {
            return prefabs.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                            .SelectMany(f => Serialize(f, prefabs)).Distinct();
        }

        IEnumerable<NetworkIdentity> Serialize(FieldInfo field, object prefabs)
        {
            var value = field.GetValue(prefabs);

            if (value == null)
                yield break;

            var fieldType = value.GetType();

            TypeInterfaces typeInterfaces;
            if(!interfaces.TryGetValue(fieldType, out typeInterfaces)) typeInterfaces = interfaces[fieldType] = new TypeInterfaces(fieldType.GetInterfaces());

            if (typeInterfaces.IsSpawnPrefabs)
                foreach (var go in Serialize(value as ISpawnPrefabs))
                    yield return go;

            if (typeInterfaces.IsSpawnSerializable)
                foreach (var go in (value as ISpawnSerializable).Serialize())
                {
                    if (go != null)
                        yield return go;
                    else
                        throw new NullPrefabException(value);
                }

            var component = value as Component;
            if (component != null)
                yield return GetIdentity(component);
        }

        static NetworkIdentity GetIdentity(Component c)
        {
            var id = c.GetComponent<NetworkIdentity>();
            return id;
        }

        struct TypeInterfaces
        {
            public readonly bool IsSpawnPrefabs;
            public readonly bool IsSpawnSerializable;

            public TypeInterfaces(Type[] interfaces)
            {
                IsSpawnPrefabs = interfaces.Any(i => i == typeof(ISpawnPrefabs));
                IsSpawnSerializable = interfaces.Any(i => i == typeof(ISpawnSerializable));
            }
        }
    }
}
