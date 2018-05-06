using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectLocator
{
    public interface IObjectLocator
    {
        void GetObjects(Vector3 position, float radius, ICollection<GameObject> outObjects);
    }
}