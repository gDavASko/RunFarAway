using System;

namespace RFW.Levels
{
    public interface ILevelController: IDisposable
    {
        ILevel CurrentLevel { get; }

        void LoadNext();
        void LoadCurrent();
    }
}