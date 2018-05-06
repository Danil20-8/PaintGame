using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Common.Network;

namespace PaintGame.Environment.NetworkElevator
{
    public class PlatformTrigger : ServerOnlyBehaviour
    {
        ElevatorBehaviour elevator;

        protected override void OnAwake()
        {
            elevator = GetComponentInParent<ElevatorBehaviour>();
        }

        void OnTriggerEnter()
        {
            elevator.Lock();
        }

        void OnTriggerExit()
        {
            elevator.UnLock();
        }
    }
}
