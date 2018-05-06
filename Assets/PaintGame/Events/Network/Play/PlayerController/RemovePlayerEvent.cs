namespace PaintGame.Events.Network.Play.PlayerController
{
    public struct RemovePlayerEvent
    {
        public readonly PaintGame.Network.Play.PlayerController Player;

        public RemovePlayerEvent(PaintGame.Network.Play.PlayerController player)
        {
            Player = player;
        }
    }
}
