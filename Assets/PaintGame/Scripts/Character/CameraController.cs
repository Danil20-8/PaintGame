using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PaintGame.Character
{
    public class CameraController : MonoBehaviour
    {
        public Vector3 rotationPoint;
        public float speed = 180f;
        public float cameraDistance = 8;

        Vector3 lookStep;

        public Transform target { get { return _target; } set { _target = value; if (_target != null) { transform.rotation = _target.rotation; } enabled = _target != null; } }
        [SerializeField]
        Transform _target;

        void Awake()
        {
            enabled = _target != null;
        }

        public void Look(Vector2 look)
        {
            lookStep = look * speed;
        }

        void Update()
        {
            var euler = transform.eulerAngles;
            if (euler.x < 180)
            {
                euler += lookStep * Time.deltaTime;
                euler.x = euler.x > 70f ? 360 + 70f : euler.x;
            }
            else if(euler.x > 180)
            {
                euler += lookStep * Time.deltaTime;
                euler.x = euler.x < 290 ? 290 : euler.x;
            }
            transform.eulerAngles = euler;
            var point = _target.localToWorldMatrix.MultiplyPoint3x4(rotationPoint);

            var back = -transform.forward;
            var resultDistance = cameraDistance;
            RaycastHit hit;
            if(Physics.Raycast(point, back, out hit, cameraDistance))
            {
                resultDistance = Mathf.Max(hit.distance - .5f, 0);
            }
            transform.position = point + back * resultDistance;
        }
    }
}
