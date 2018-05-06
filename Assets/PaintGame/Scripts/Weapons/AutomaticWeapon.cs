using PaintGame.Character;
using PaintGame.Core.Spawn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Weapons
{
    public class AutomaticWeapon : OldWeapon, ISpawnSerializable
    {
        public WeaponBall ballPrefab;

        public Transform firePoint;
        public int hitDamage;
        public float ballSpeed;

        protected override void OnFire(CombatController shooter)
        {
            var ball = PGServiceLocator.SpawnManager.SpawnPrefab(ballPrefab, firePoint.position, firePoint.rotation, InitializeBall);

            ball.AddTrigger(shooter, hitDamage);
        }

        void InitializeBall(WeaponBall ball)
        {
            ball.SetVelocity(firePoint.forward * ballSpeed);
        }

        IEnumerable<NetworkIdentity> ISpawnSerializable.Serialize()
        {
            yield return ballPrefab.GetComponent<NetworkIdentity>();
        }
    }
}
