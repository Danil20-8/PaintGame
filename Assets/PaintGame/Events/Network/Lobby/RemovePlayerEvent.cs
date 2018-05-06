using UnityEngine.Networking;

namespace PaintGame.Events.Network.Lobby
{
    public struct RemovePlayerEvent
    {
        public readonly NetworkLobbyPlayer Player;

        public RemovePlayerEvent(NetworkLobbyPlayer player)
        {
            Player = player;
        }
    }
}
