using Cysharp.Threading.Tasks;
using UnityEngine;

namespace RFW
{
    public class UnitsFactory : IUnitsFactory
    {
        private IGettableAsset _assetGetter = null;

        public UnitsFactory(IGettableAsset assetGetter)
        {
            _assetGetter = assetGetter;
        }

        public virtual async UniTask<T> CreateUnitAsync<T>(string unitId, Vector3 position)
            where T :class, IUnitView
        {
            IUnitView view = await _assetGetter.LoadResource<IUnitView>(unitId);

            if (view == null)
            {
                Debug.LogError($"[{nameof(UnitsFactory)}] Try to create Unit with" +
                               $" Non exists view! Id is <{unitId}>");
                return null;
            }

            view.transform.position = position;

            return view as T;
        }
    }
}