using System;

namespace RFW.Pool
{
    public interface IPoolable<T>: IInitializable, IDestroyable, IDisposable where T: IPoolable<T>
    {
        Type Type
        {
            get;
        }
        void SetReleaser(IReleaser<T> releaser);
        void Release();
    }
}