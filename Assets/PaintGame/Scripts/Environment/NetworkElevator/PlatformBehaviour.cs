using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace PaintGame.Environment.NetworkElevator
{
    public class PlatformBehaviour : MonoBehaviour
    {
        [SerializeField]
        Transform startPoint;
        [SerializeField]
        Transform endPoint;

        new Transform transform;
        Transform world;

        void Awake()
        {
            transform = base.transform;

            world = transform.parent;

            startPoint.SetParent(world);
            transform.SetParent(startPoint);
        }

        public Transform GetEndPoint(int level)
        {
            var point = level % 2 == 0 ? startPoint : endPoint;

            var curr = transform.parent;

            transform.SetParent(world);

            curr.SetParent(transform);

            point.SetParent(world);

            transform.SetParent(point);

            return point;
        }
    }
}
