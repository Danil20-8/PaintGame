using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using PaintGame.Core;
using PaintGame.Character;
using PaintGame.Core.PGInput;
using PaintGame.View;
using SpecialInput;
using PaintGame.Weapons;
using PaintGame.Events;
using PaintGame.Events.Network.Play.PlayerCharacter;

namespace PaintGame.Character
{
    public class PlayerCharacter : NetworkBehaviour
    {

        IPlayerInput playerInput;

        Movement movement;
        CameraController cameraController;
        Transform cameraTransform;
        CombatController combatController;

        PlayerView playerView;

        public Transform hand { get { return movement.handTransform; } }

        public int hits { get { return combatController.hits; } }

        void Awake()
        {
            movement = GetComponent<Movement>();
            combatController = GetComponent<CombatController>();

            GlobalEventListener.Raise(new AwakeLocalCharacterEvent());
        }

        public override void OnStartAuthority()
        {
            playerView = FindObjectOfType<PlayerView>();


            playerInput = PGServiceLocator.instance.GetPlayerInput();

            cameraTransform = Camera.main.transform;

            cameraController = cameraTransform.gameObject.AddComponent<CameraController>();
            cameraController.rotationPoint = new Vector3(0, 4, 0);
            cameraController.target = transform;
        }

        void Update()
        {
            if (!hasAuthority)
                return;

            var ltow = transform.localToWorldMatrix;

            movement.Move(ltow.MultiplyVector(playerInput.Move().XZ()));
            movement.Look(cameraTransform.forward);
            movement.Jump(playerInput.Jump());

            combatController.Fire(playerInput.Fire());

            cameraController.Look(playerInput.Look());

            playerView.SetScore(combatController.hits);
        }

        public void PickUpWeapon(Weapon weapon)
        {
            weapon = PGServiceLocator.SpawnManager.SpawnPrefab(weapon, hand.position, hand.rotation, InitializeWeapon);
            RpcPickUpWeapon(weapon.GetComponent<NetworkIdentity>());
        }

        static void InitializeWeapon(Weapon weapon)
        {

        }

        [ClientRpc]
        void RpcPickUpWeapon(NetworkIdentity weaponIdentity)
        {
            var weapon = weaponIdentity.GetComponent<Weapon>();

            var weaponTransform = weapon.transform;
            weaponTransform.SetParent(hand);
            weaponTransform.localPosition = Vector3.zero;
            weaponTransform.localRotation = Quaternion.identity;

            if(combatController.weapon != null)
                Destroy(combatController.weapon.gameObject);
            combatController.weapon = weapon;
        }
    }
}
