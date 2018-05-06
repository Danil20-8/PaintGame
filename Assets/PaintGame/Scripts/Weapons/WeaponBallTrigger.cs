using PaintGame.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PaintGame.Weapons
{
    public class WeaponBallTrigger : MonoBehaviour
    {
        CombatController owner;

        int hitDamage;

        public void Fire(CombatController owner, int hitDamage)
        {
            this.owner = owner;
            this.hitDamage = hitDamage;
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.transform.IsOne(owner.transform))
                return;

            var hitedCombat = col.gameObject.GetComponentInParent<CombatController>();

            if (hitedCombat != null)
            {
                owner.TakeHitsFrom(hitedCombat, hitDamage);
            }

            Destroy(gameObject);
        }
    }
}
