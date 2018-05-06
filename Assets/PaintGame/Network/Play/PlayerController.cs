using System;
using UnityEngine;
using UnityEngine.Networking;
using PaintGame.Character;
using PaintGame.Core.Exceptions;
using PaintGame.Events;
using PaintGame.Events.Network.Play.PlayerController;

namespace PaintGame.Network.Play
{
    public class PlayerController : NetworkBehaviour
    {
        public PlayerCharacter character { get; private set; }

        public void Spawn(Transform spawnTransform)
        {
            if (!isServer)
                throw new ServerOnlyException(this);
            if (character == null)
            {
                character = PGServiceLocator.SpawnManager.SpawnPlayerFor(p => p.PlayerPrefabs.Player, spawnTransform.position, spawnTransform.rotation, Initialize, gameObject);
                character.PickUpWeapon(PGServiceLocator.SpawnManager.GetPrefab(p => p.WeaponPrefabs.Weapon));
                RpcSpawn(character.GetComponent<NetworkIdentity>());
            }
            else
                throw new Exception(string.Format("{0} already has spawned character", this));
        }

        [ClientRpc]
        void RpcSpawn(NetworkIdentity characterIdentity)
        {
            
            character = characterIdentity.GetComponent<PlayerCharacter>();
            if(isLocalPlayer)
            {
                var camera = FindObjectOfType<CameraController>();
                camera.target = character.transform;
            }

            GlobalEventListener.Raise(new PlayerReadyEvent());
        }

        static void Initialize(PlayerCharacter character)
        {
        }

        protected void Start()
        {
            GlobalEventListener.Raise(new AddPlayerEvent(this));
        }

        protected void OnDestroy()
        {
            GlobalEventListener.Raise(new RemovePlayerEvent(this));
        }
    }
}
