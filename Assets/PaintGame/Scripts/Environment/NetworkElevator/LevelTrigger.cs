using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Common.Network;
namespace PaintGame.Environment.NetworkElevator
{
    public class LevelTrigger : ServerOnlyBehaviour
    {
        LevelBehaviour level;
        ElevatorBehaviour elevator;

        void OnTriggerEnter()
        {
            elevator.SignalOnLevel(level);
        }

        void OnTriggerExit()
        {
            elevator.SignalOutLevel(level);
        }

        protected override void OnAwake()
        {
            level = GetComponentInParent<LevelBehaviour>();

            elevator = GetComponentInParent<ElevatorBehaviour>();
        }
    }
}
