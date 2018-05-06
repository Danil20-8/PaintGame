using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaintGame.Common
{
    public class BillboardBehaviour : MonoBehaviour
    {

        new Transform camera;

        void Start()
        {
            camera = Camera.main.transform;
        }

        void Update()
        {
            transform.rotation = Quaternion.LookRotation(camera.position - transform.position);
        }

        void OnBecomeVisible()
        {
            enabled = true;
        }

        void OnBecomeInvisible()
        {
            enabled = false;
        }
    }
}