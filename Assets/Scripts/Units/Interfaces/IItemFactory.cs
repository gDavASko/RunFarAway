using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RFW
{
    public interface IItemFactory: IUnitsFactory
    {
        public UniTask<UnitBase> CreateItemAsync(string unitId, Vector3 position);
    }
}