using System;

namespace RFW
{
    public interface IUnit : IDisposable, IInitializable
    {
        IUnitView UnitView
        {
            get;
        }

        T GetSystem<T>() where T :class, IUnitSystem;
    }
}