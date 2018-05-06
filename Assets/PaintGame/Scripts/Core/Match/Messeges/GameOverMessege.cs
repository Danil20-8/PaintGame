using PaintGame.Network.Play;

namespace PaintGame.Core.Match.Messeges
{
    public struct GameOverMessege
    {
        public readonly PlayerController Winner;
        public readonly bool TimeOut;

        public IGameOverChecker Checker;

        public GameOverMessege(IGameOverChecker checker, PlayerController winner, bool timeOut)
        {
            Checker = checker;
            Winner = winner;
            TimeOut = timeOut;
        }
    }
}
