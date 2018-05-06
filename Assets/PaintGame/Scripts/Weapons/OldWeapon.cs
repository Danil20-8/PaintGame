using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using PaintGame.Core.Spawn;
using PaintGame.Character;

namespace PaintGame.Weapons
{
    public abstract class OldWeapon : MonoBehaviour
    {
        [SerializeField]
        float cooldown;

        float lastShot;

        protected void Awake()
        {
            cooldown *= Time.fixedDeltaTime;
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        public bool IsReady()
        {
            return Time.fixedTime - lastShot > cooldown;
        }

        public void Fire(CombatController shooter)
        {
            var time = Time.fixedTime;
            if (time - lastShot > cooldown)
            {
                OnFire(shooter);
                lastShot = time;
            }
        }

        protected abstract void OnFire(CombatController shooter);
    }
}
