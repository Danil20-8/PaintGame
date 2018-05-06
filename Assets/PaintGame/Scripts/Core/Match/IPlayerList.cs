using PaintGame.Network.Play;

namespace PaintGame.Core.Match
{
    public interface IPlayerList
    {
        PlayerController[] Players { get; }
    }
}
