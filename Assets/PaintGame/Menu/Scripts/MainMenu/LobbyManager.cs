using PaintGame.Events;
using PaintGame.Events.Network.Lobby;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace PaintGame.Menu.MainMenu
{
    using MenuAddPlayerEvent = Events.Menu.MainMenu.Lobby.AddPlayerEvent;
    using MenuRemovePlayerEvent = Events.Menu.MainMenu.Lobby.RemovePlayerEvent;
    using MenuPlayerReadyEvent = Events.Menu.MainMenu.Lobby.PlayerReadyEvent;

    public class LobbyManager : MonoBehaviour
    {
        bool isHost;

        List<NetworkLobbyPlayer> players = new List<NetworkLobbyPlayer>();

        public ReadOnlyCollection<NetworkLobbyPlayer> GetPlayers()
        {
            return players.AsReadOnly();
        }

        public void Ready(bool ready)
        {
            player.readyToBegin = ready;

            if (ready && !isHost)
            {
                player.SendReadyToBeginMessage();
            }
            else if (!ready)
            {
                player.SendNotReadyToBeginMessage();
            }
            if(isHost)
            {
                EventSpammer.SpamDown(gameObject, new MenuPlayerReadyEvent(player, ready));
            }
        }

        public void StartGame()
        {
            if (player.readyToBegin && isHost)
                player.SendReadyToBeginMessage();
        }

        public void StartHost()
        {
            lobby.StartHost();
            isHost = true;
        }

        public bool StartClient(string address)
        {
            return StartClient(address, lobby.networkPort);
        }

        public bool StartClient(string address, int port)
        {
            lobby.networkAddress = address;
            lobby.networkPort = port;

            var client = lobby.StartClient();
            return true;
        }

        public void LeaveLobby()
        {
            if (isHost)
            {
                lobby.StopHost();
                isHost = false;
            }
            else
            {
                lobby.StopClient();
            }
        }

        private NetworkLobbyManager lobby { get { return NetworkLobbyManager.singleton as NetworkLobbyManager; } }
        private NetworkLobbyPlayer player { get { return players.Select(o => o.GetComponent<NetworkLobbyPlayer>()).First(p => p != null && p.isLocalPlayer); } }

        protected void Start()
        {
            GlobalEventListener.Listen<PlayerReadyEvent>(PlayerReadyListener);
            GlobalEventListener.Listen<AddPlayerEvent>(AddPlayerListener);
            GlobalEventListener.Listen<RemovePlayerEvent>(RemovePlayerListener);
        }

        void PlayerReadyListener(PlayerReadyEvent e)
        {
            EventSpammer.SpamDown(gameObject, new MenuPlayerReadyEvent(e.Player, e.Ready));
        }

        void AddPlayerListener(AddPlayerEvent e)
        {
            players.Add(e.Player);

            EventSpammer.SpamDown(gameObject, new MenuAddPlayerEvent(e.Player));
        }

        void RemovePlayerListener(RemovePlayerEvent e)
        {
            players.Remove(e.Player);

            EventSpammer.SpamDown(gameObject, new MenuRemovePlayerEvent(e.Player));
        }

        protected void OnDestroy()
        {
            GlobalEventListener.StopListen<PlayerReadyEvent>(PlayerReadyListener);
            GlobalEventListener.StopListen<AddPlayerEvent>(AddPlayerListener);
            GlobalEventListener.StopListen<RemovePlayerEvent>(RemovePlayerListener);
        }
    }
}
