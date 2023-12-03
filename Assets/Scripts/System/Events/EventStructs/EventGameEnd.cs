namespace RFW.Events
{
    public struct EventGameEnd : IEvent
    {
        public GameResult Result;

        public EventGameEnd(GameResult result)
        {
            Result = result;
        }
    }

    public enum GameResult
    {
        Victory = 0,
        Lose = 1,
        Restart = 2,
    }
}