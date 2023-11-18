using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RFW
{
    public interface IUnitsFactory
    {
        UniTask<T> CreateUnitAsync<T>(string unitId, Vector3 position)
            where T : class, IUnitView;
    }
}