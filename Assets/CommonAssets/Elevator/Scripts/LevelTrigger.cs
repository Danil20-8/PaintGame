using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Elevator
{
    public class LevelTrigger : MonoBehaviour
    {
        void OnTriggerEnter()
        {
            GetComponentInParent<ElevatorBehaviour>().SignalLevel(GetComponentInParent<LevelBehaviour>(), 1);
        }
    }
}
