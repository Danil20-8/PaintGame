using UnityEngine.Networking;

namespace PaintGame.Events.Menu.MainMenu.Lobby
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
