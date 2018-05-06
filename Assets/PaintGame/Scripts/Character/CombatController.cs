using PaintGame.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Character
{
    public class CombatController : NetworkBehaviour
    {

        public Weapon weapon;

        public const int maxHits = 100;

        [SyncVar]
        public int hits = maxHits;

        float fire;

        public void Fire(float fire)
        {
            this.fire = fire;
        }

        public void TakeHitsFrom(CombatController combat, int amount)
        {
            amount = amount < combat.hits ? amount : combat.hits;
            combat.hits -= amount;
            this.hits += amount;
        }

        [Command]
        void CmdFire()
        {
            weapon.Fire(this);
        }

        void FixedUpdate()
        {
            if(fire == 1f)
            {
                if (weapon.IsReady())
                    CmdFire();
            }
        }
    }
}
