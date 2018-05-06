using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Common.Network;
namespace PaintGame.Environment.NetworkVerticalElevator
{
    public class VerticalElevatorTrigger : ServerOnlyBehaviour
    {
        VerticalElevator ve;

        protected override void OnAwake()
        {
            ve = GetComponentInParent<VerticalElevator>();
        }

        void OnTriggerEnter(Collider col)
        {
            ve.SignalUp();
        }

        void OnTriggerExit(Collider col)
        {
            ve.SignalDown();
        }
    }
}
