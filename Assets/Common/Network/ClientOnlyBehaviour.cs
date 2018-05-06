using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Common.Network
{
    public class ClientOnlyBehaviour : MonoBehaviour
    {
        public MonoBehaviour[] ServerOnly;

        public BehaviourRemoveType type;

        protected void Awake()
        {
            ServerIdentifer.IsClient(isClient =>
            {
                if (isClient)
                {
                    OnAwake();
                }
                else
                {
                    if (type == BehaviourRemoveType.Selected)
                        foreach (var b in ServerOnly)
                            Destroy(b);
                    if (type == BehaviourRemoveType.All)
                        foreach (var b in GetComponents<MonoBehaviour>())
                            Destroy(b);
                    if (type == BehaviourRemoveType.AllIncludeChilds)
                        foreach (var b in GetComponentsInChildren<MonoBehaviour>())
                            Destroy(b);
                    Destroy(this);
                }
            });
        }
        protected virtual void OnAwake()
        {

        }
    }
}
