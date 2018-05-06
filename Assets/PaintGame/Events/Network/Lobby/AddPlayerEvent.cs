using UnityEngine.Networking;

namespace PaintGame.Events.Network.Lobby
{
    public struct AddPlayerEvent
    {
        public readonly NetworkLobbyPlayer Player;

        public AddPlayerEvent(NetworkLobbyPlayer player)
        {
            Player = player;
        }
    }
}
