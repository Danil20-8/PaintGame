using PaintGame.Events;
using PaintGame.Events.Network.Play.PlayerController;
using PaintGame.Events.Network.Play.PlayerList;
using PaintGame.Network.Play;
using System.Linq;
using UnityEngine;

namespace PaintGame.Core.Match
{
    public class PlayerList : MonoBehaviour, IPlayerList
    {
        public PlayerController[] Players { get { return players; } }
        private PlayerController[] players = new PlayerController[0];

        private int readyPlayerCount;

        protected void Start()
        {
            GlobalEventListener.Listen<AddPlayerEvent>(AddPlayerListener);
            GlobalEventListener.Listen<RemovePlayerEvent>(RemovePlayerListener);
            GlobalEventListener.Listen<PlayerReadyEvent>(PlayerReadyListener);
        }

        void AddPlayerListener(AddPlayerEvent e)
        {
            players = players.Concat(new[] { e.Player }).ToArray();
        }

        void RemovePlayerListener(RemovePlayerEvent e)
        {
            players = players.Where(p => p != e.Player).ToArray();
        }

        void PlayerReadyListener(PlayerReadyEvent e)
        {
            ++readyPlayerCount;
            if (readyPlayerCount == players.Length)
                GlobalEventListener.Raise(new AllPlayersReadyEvent());
        }
    }
}
