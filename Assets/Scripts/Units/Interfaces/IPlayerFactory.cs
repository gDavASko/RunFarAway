using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RFW
{
    public interface IPlayerFactory: IUnitsFactory
    {
        public UniTask<UnitBase> CreatePlayerAsync(Vector3 position);
    }
}