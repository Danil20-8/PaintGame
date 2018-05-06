using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Common.Network;
using PaintGame.Character;

namespace PaintGame.Environment.WeaponPicker
{
    public class WeaponPickerTrigger : ServerOnlyBehaviour
    {
        private WeaponPicker wp;

        protected override void OnAwake()
        {
            wp = GetComponentInParent<WeaponPicker>();
        }

        void OnTriggerEnter(Collider col)
        {
            var player = col.GetComponentInParent<PlayerCharacter>();
            if (player != null)
                wp.PickUp(player);
        }
    }
}
