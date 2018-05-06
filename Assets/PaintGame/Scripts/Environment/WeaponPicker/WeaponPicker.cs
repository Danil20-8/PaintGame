using System;
using System.Collections;
using System.Linq;
using System.Text;
using Common.Network;
using UnityEngine;
using UnityEngine.Networking;
using PaintGame.Character;
using PaintGame.Weapons;

namespace PaintGame.Environment.WeaponPicker
{
    public class WeaponPicker : NetworkBehaviour
    {
        [SerializeField]
        Weapon weaponPrefab;

        [SerializeField]
        WeaponPickerView weaponView;

        [SerializeField]
        float time = 10;

        bool active;

        void Awake()
        {
            weaponView = GetComponentInChildren<WeaponPickerView>();
            weaponView.InitializeView(weaponPrefab);
        }
        void Start()
        {
            StartCoroutine(Cooldown());
        }

        public void PickUp(PlayerCharacter player)
        {
            if (active)
            {
                player.PickUpWeapon(weaponPrefab);
                RpcPickedUp();
            }
        }

        [ClientRpc]
        void RpcPickedUp()
        {
            StartCoroutine(Cooldown());
        }

        IEnumerator Cooldown()
        {
            active = false;
            weaponView.gameObject.SetActive(false);

            yield return new WaitForSeconds(time);

            active = true;
            weaponView.gameObject.SetActive(true);
        }
    }
}
