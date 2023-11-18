using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RFW
{
    public interface IPlayerFactory: IUnitsFactory
    {
        public UniTask<IUnitView> CreatePlayerAsync(Vector3 position);
    }
}