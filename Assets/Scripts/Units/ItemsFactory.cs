using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RFW.Events;
using UnityEngine;

namespace RFW
{
    public class ItemsFactory : UnitsFactory, IItemFactory
    {
        public ItemsFactory(IGettableAsset assetGetter, UnitEvents unitEvents): base(assetGetter, unitEvents)
        {
        }

        public async UniTask<UnitBase> CreateItemAsync(string unitId, Vector3 position)
        {
            List<IUnitSystem> systems = new List<IUnitSystem>();

            //add items systems

            UnitBase unit = await CreateUnitAsync<UnitBase>(unitId, systems, position);

            _unitEvents.OnUnitCreated?.Invoke(unit);
            return unit;
        }
    }
}