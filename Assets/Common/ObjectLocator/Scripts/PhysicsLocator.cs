using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ObjectLocator
{
    public class PhysicsLocator : MonoBehaviour, IObjectLocator
    {
        [SerializeField]
        int maxHits = 100;

        [SerializeField]
        LayerMask layerMask;

        Collider[] result;

        protected virtual void Awake()
        {
            result = new Collider[maxHits];
        }

        public void GetObjects(Vector3 position, float radius, ICollection<GameObject> outObjects)
        {
            var hits = Physics.OverlapSphereNonAlloc(position, radius, result, layerMask);
            for (int i = 0; i < hits; ++i)
                outObjects.Add(result[i].gameObject);
        }
    }
}
