using System;
using UnityEngine;

namespace RFW.Pool
{
    public interface IPooledComponentProvider<T>: IReleaser<T>, IDisposable where T : MonoBehaviour, IPoolable<T>
    {
        T Get();
    }
}