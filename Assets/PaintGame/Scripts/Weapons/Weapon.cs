using PaintGame.Character;
using PaintGame.Core.Spawn;
using System.Linq;
using UnityEngine;

using Random = UnityEngine.Random;
using System;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace PaintGame.Weapons
{
    public class Weapon : MonoBehaviour, ISpawnSerializable
    {
        [SerializeField]
        WeaponBall ballPrefab;

        [SerializeField]
        Transform firePoint;

        [SerializeField]
        float velocity;
        [SerializeField]
        float radiusdgr;
        [SerializeField]
        float intencity;
        [SerializeField]
        int shotAmount;
        [SerializeField]
        int damage;

        float lastShot;

        void Initialize()
        {
            var vals = Enumerable.Range(0, 5).Select(i => Random.value).ToArray();
            var sum = vals.Sum() *.5f;

            velocity *= vals[0] / sum;
            radiusdgr = Mathf.Max(0, radiusdgr - radiusdgr * vals[1] / sum);
            intencity = Mathf.Max(0, intencity - intencity * vals[2] / sum);
            shotAmount = (int) (shotAmount * vals[3] / sum) + 1;
            damage = (int) (damage * vals[4] / sum) + 1;
        }

        public bool IsReady()
        {
            return Time.fixedTime - lastShot > intencity;
        }

        public void Fire(CombatController shooter)
        {
            var time = Time.fixedTime;
            if (time - lastShot > intencity)
            {

                for (int i = 0; i < shotAmount; ++i)
                {
                    var ball = PGServiceLocator.SpawnManager.SpawnPrefab(ballPrefab, firePoint.position, firePoint.rotation, InitializeBall);

                    ball.AddTrigger(shooter, damage);
                }

                lastShot = time;
            }
        }

        void InitializeBall(WeaponBall ball)
        {
            var qRight = Quaternion.AngleAxis(Random.Range(0f, 360f), firePoint.forward);
            var right = qRight * firePoint.right;
            var dir = Vector3.Lerp(firePoint.forward, right, radiusdgr / 90f * Random.value).normalized;

            ball.SetVelocity(dir * velocity);
        }

        protected void Start()
        {
            Initialize();
        }

        public IEnumerable<NetworkIdentity> Serialize()
        {
            yield return ballPrefab.GetComponent<NetworkIdentity>();
        }
    }
}
