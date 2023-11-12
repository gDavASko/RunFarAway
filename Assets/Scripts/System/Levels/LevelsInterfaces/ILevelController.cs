namespace RFW.Levels
{
    public interface ILevelController
    {
        ILevel CurrentLevel { get; }

        void LoadNext();
        void LoadCurrent();
    }
}