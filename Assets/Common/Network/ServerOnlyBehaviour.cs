using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
namespace Common.Network
{
    public class ServerOnlyBehaviour : MonoBehaviour
    {
        public BehaviourRemoveType type = BehaviourRemoveType.Self;

        public MonoBehaviour[] ClientOnly;

        static Type[] NetworkImportant = new Type[]
        {
            typeof(NetworkIdentity),
            typeof(NetworkTransform)
        };

        protected void Awake()
        {
            ServerIdentifer.IsServer(isServer =>
            {
                if (isServer)
                {
                    OnAwake();
                }
                else
                {
                    if (type == BehaviourRemoveType.Selected)
                        foreach (var b in ClientOnly)
                            Destroy(b);
                    if (type == BehaviourRemoveType.All)
                        foreach (var b in GetComponents<MonoBehaviour>().Where(b => !NetworkImportant.Contains(b.GetType())))
                            Destroy(b);
                    if (type == BehaviourRemoveType.AllIncludeChilds)
                        foreach (var b in GetComponentsInChildren<MonoBehaviour>().Where(b => !NetworkImportant.Contains(b.GetType())))
                            Destroy(b);
                    Destroy(this);
                }
            });
        }

        protected virtual void OnAwake()
        {

        }
    }

    public enum BehaviourRemoveType
    {
        Self,
        Selected,
        All,
        AllIncludeChilds
    }
}
