using System.Collections.Generic;
using PaintGame.Character;
using PaintGame.Core.Spawn;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Weapons
{
    public class ShotgunWeapon : OldWeapon, ISpawnSerializable
    {
        [SerializeField]
        WeaponBall ballPrefab;
        [SerializeField]
        Transform firePoint;
        [SerializeField]
        int hitDamage;
        [SerializeField]
        float ballSpeed;
        [SerializeField]
        int ballCount;
        [SerializeField]
        float angle;

        protected override void OnFire(CombatController shooter)
        {
            for (int i = 0; i < ballCount; ++i)
            {
                var ball = PGServiceLocator.SpawnManager.SpawnPrefab(ballPrefab, firePoint.position, firePoint.rotation, InitializeBall);

                ball.AddTrigger(shooter, hitDamage);
            }
        }

        void InitializeBall(WeaponBall ball)
        {
            var qRight = Quaternion.AngleAxis(Random.Range(0f, 360f), firePoint.forward);
            var right = qRight * firePoint.right;
            var dir = Vector3.Lerp(firePoint.forward, right, angle / 90f * Random.value).normalized;

            ball.SetVelocity(dir * ballSpeed);
        }

        IEnumerable<NetworkIdentity> ISpawnSerializable.Serialize()
        {
            yield return ballPrefab.GetComponent<NetworkIdentity>();
        }
    }
}
