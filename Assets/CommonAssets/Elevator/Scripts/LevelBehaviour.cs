using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Elevator
{
    public class LevelBehaviour : MonoBehaviour
    {

        [SerializeField]
        Transform rotator;

        Action completedCallback;
        float speed;
        Quaternion endPoint;

        Transform platform;

        void Awake()
        {
            enabled = false;
        }

        public void SetRotatorFor(LevelBehaviour level)
        {
            rotator.localRotation = GetGoalLocalRotation(level);
        }

        public void SetPlatform(Transform platform)
        {
            this.platform = platform;
            platform.SetParent(rotator, true);
        }


        public void TransitTo(LevelBehaviour level, float speedDegrees, Action completedCallback)
        {
            endPoint = GetGoalLocalRotation(level);

            TransitTo(endPoint, speedDegrees, completedCallback);
        }

        Quaternion GetGoalLocalRotation(LevelBehaviour level)
        {
            return WorldToLocalRotation(Quaternion.LookRotation(level.rotator.position - rotator.position));
        }


        public void TransitToIdentity(float speedDegrees, Action completedCallback)
        {
            TransitTo(Quaternion.identity, speedDegrees, completedCallback);
        }

        Quaternion WorldToLocalRotation(Quaternion rotation)
        {
            return Quaternion.Inverse(transform.rotation) * rotation;
        }

        void TransitTo(Quaternion point, float speedDegrees, Action completedCallback)
        {
            this.completedCallback = completedCallback;
            endPoint = point;

            speed = speedDegrees * Time.fixedDeltaTime;
            enabled = true;
        }

        void FixedUpdate()
        {
            rotator.localRotation = WorldToLocalRotation(Quaternion.LookRotation(platform.position - rotator.position));
            var dot = Quaternion.Dot(rotator.localRotation, endPoint);
            if(dot >= 1f)
            {
                rotator.localRotation = endPoint;
                enabled = false;
                completedCallback();
            }
            else
            {
                rotator.localRotation = Quaternion.Slerp(rotator.localRotation, endPoint, speed / Mathf.Acos(dot));
            }
        }
    }
}