using PaintGame.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    public class WeaponBall : MonoBehaviour
    {
        private Rigidbody rigidBody;

        protected void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        public void AddTrigger(CombatController owner, int hitDamage)
        {
            gameObject.AddComponent<WeaponBallTrigger>().Fire(owner, hitDamage);
        }

        public void SetVelocity(Vector3 velocity)
        {
            rigidBody.velocity = velocity;
        }
    }
}
