using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PaintGame.Character
{
    public class GroundChecker : MonoBehaviour
    {
        int grounded;

        public bool IsGrounded()
        {
            return grounded > 0;
        }

        void OnTriggerEnter(Collider col)
        {
            ++grounded;
        }
        void OnTriggerExit(Collider col)
        {
            --grounded;
        }
    }
}
