using PaintGame.Events;
using PaintGame.Events.Network.Lobby;
using UnityEngine.Networking;

namespace PaintGame.Network.Lobby
{
    public class LobbyPlayerBehaviour : NetworkLobbyPlayer
    {
        public override void OnClientReady(bool readyState)
        {
            GlobalEventListener.Raise(new PlayerReadyEvent(this, readyState));
        }
        public override void OnClientEnterLobby()
        {
            GlobalEventListener.Raise(new AddPlayerEvent(this));
        }
        public override void OnClientExitLobby()
        {
            GlobalEventListener.Raise(new RemovePlayerEvent(this));
        }
    }
}
