using PaintGame.Events;
using PaintGame.Events.Menu.MainMenu.Lobby;
using PaintGame.Menu.MainMenu.Lobby;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace PaintGame.Menu.MainMenu
{
    public class LobbyBehaviour : MainMenuPageBehaviour
    {
        [SerializeField]
        private Toggle readyToggle;

        [SerializeField]
        private Transform playerList;
        [SerializeField]
        private PlayerListItemBehaviour playerListItemBehaviourPrefab;

        public void ToggleReady(bool ready)
        {
            GetComponentInParent<LobbyManager>().Ready(ready);
        }

        public void ClickStart()
        {
            GetComponentInParent<LobbyManager>().StartGame();
        }

        public void ClickLeave()
        {
            GetComponentInParent<LobbyManager>().LeaveLobby();
            MainMenu.GoToStart();
        }

        protected void Awake()
        {
            readyToggle.onValueChanged.AddListener(ToggleReady);

            var listener = gameObject.AddComponent<EventListenerBehaviour>();

            listener.Listen<AddPlayerEvent>(AddPlayerListener);
            listener.Listen<RemovePlayerEvent>(RemovePlayerListener);
            listener.Listen<PlayerReadyEvent>(PlayerReadyListener);

            var lobby = GetComponentInParent<LobbyManager>();

            foreach(var player in lobby.GetPlayers())
            {
                AddPlayerItem(player);
            }
        }

        Dictionary<NetworkLobbyPlayer, PlayerListItemBehaviour> playerItemMap = new Dictionary<NetworkLobbyPlayer, PlayerListItemBehaviour>();

        void AddPlayerListener(AddPlayerEvent e)
        {
            AddPlayerItem(e.Player);
        }

        void AddPlayerItem(NetworkLobbyPlayer player)
        {
            var listItem = Instantiate(playerListItemBehaviourPrefab, playerList);

            listItem.SetPlayerName(player.name);
            listItem.SetReady(player.readyToBegin);

            playerItemMap.Add(player, listItem);
        }

        void RemovePlayerListener(RemovePlayerEvent e)
        {
            var listItem = playerItemMap[e.Player];
            playerItemMap.Remove(e.Player);

            Destroy(listItem.gameObject);
        }

        void PlayerReadyListener(PlayerReadyEvent e)
        {
            playerItemMap[e.Player].SetReady(e.Ready);
        }
    }
}
