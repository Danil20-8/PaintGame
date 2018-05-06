using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace ObjectLocator
{
    public class SimpleLocator : MonoBehaviour, IObjectLocator
    {

        List<Transform> objects = new List<Transform>();

        public void RegisterObject(GameObject gameObject)
        {
            objects.Add(gameObject.transform);
        }
        public void UnRegisterObject(GameObject gameObject)
        {
            objects.Remove(gameObject.transform);
        }

        public void GetObjects(Vector3 position, float radius, ICollection<GameObject> outObjects)
        {
            var sqrRadius = radius * radius;
            foreach(var o in objects)
            {
                if ((o.position - position).sqrMagnitude < sqrRadius)
                    outObjects.Add(o.gameObject);
            }
        }
    }
}
