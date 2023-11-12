namespace RFW.Events
{
    public class GameEvents
    {
        public System.Action OnGameLoaded { get; set; }
        public System.Action OnGameStart { get; set; }
        public System.Action OnNextGame { get; set; }
        public System.Action OnRestartGame { get; set; }
        public System.Action OnToMainMenu { get; set; }
        public System.Action<GameResult> OnGameFinish { get; set; }

        public enum GameResult
        {
            Victory = 0,
            Death = 1,
        }
    }
}