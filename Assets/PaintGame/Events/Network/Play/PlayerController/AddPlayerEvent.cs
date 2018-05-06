namespace PaintGame.Events.Network.Play.PlayerController
{
    public struct AddPlayerEvent
    {
        public readonly PaintGame.Network.Play.PlayerController Player;

        public AddPlayerEvent(PaintGame.Network.Play.PlayerController player)
        {
            Player = player;
        }
    }
}
