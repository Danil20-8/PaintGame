namespace PaintGame.Core.Match.Messeges
{
    public struct StartGameTimerMessege
    {
        public readonly bool Started;
        public readonly float Rest;
        public readonly float Full;

        public StartGameTimerMessege(bool started, float rest, float full)
        {
            Started = started;
            Rest = rest;
            Full = full;
        }
    }
}
