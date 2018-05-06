using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Elevator {
    public class PlatformTrigger : MonoBehaviour {

        void OnTriggerEnter(Collider col)
        {
            GetComponentInParent<ElevatorBehaviour>().Lock();
        }

        void OnTriggerExit(Collider col)
        {
            GetComponentInParent<ElevatorBehaviour>().UnLock();
        }
    }
}