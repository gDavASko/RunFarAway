using Cysharp.Threading.Tasks;
using RFW.Events;
using UnityEngine;

namespace RFW
{
    public class UnitsFactory : IUnitsFactory
    {
        private IGettableAsset _assetGetter = null;
        protected UnitEvents _unitEvents = null;

        public UnitsFactory(IGettableAsset assetGetter, UnitEvents unitEvents)
        {
            _assetGetter = assetGetter;
            _unitEvents = unitEvents;
        }

        public virtual async UniTask<T> CreateUnitAsync<T>(string unitId, params object[] parameters)
            where T :class, IUnit
        {
            IUnitView view = await _assetGetter.LoadResource<IUnitView>(unitId);

            if (view == null)
            {
                Debug.LogError($"[{nameof(UnitsFactory)}] Try to create Unit with" +
                               $" Non exists view! Id is <{unitId}>");
                return null;
            }

            IUnit unit = new UnitBase();
            unit.Init(view, parameters, _unitEvents);

            _unitEvents.OnUnitCreated?.Invoke(unit);

            return unit as T;
        }
    }
}