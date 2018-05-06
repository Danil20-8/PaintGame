using UnityEngine.Networking;

namespace PaintGame.Events.Menu.MainMenu.Lobby
{
    public struct PlayerReadyEvent
    {
        public readonly NetworkLobbyPlayer Player;
        public readonly bool Ready;

        public PlayerReadyEvent(NetworkLobbyPlayer player, bool ready)
        {
            Player = player;
            Ready = ready;
        }
    }
}
