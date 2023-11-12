using UnityEngine;

namespace RFW.Pool
{
    public interface IPoolObjects<T> where T : MonoBehaviour, IPoolable<T>
    {
    }
}