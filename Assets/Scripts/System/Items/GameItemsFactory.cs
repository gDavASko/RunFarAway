using System.Collections.Generic;
using System.Threading.Tasks;
using RFW.Events;
using UnityEngine;

namespace RFW
{
    public class GameItemsFactory : IGameItemsFactory
    {
        //ToDo: move cache logic to pool controller
        private ItemEvents _itemEvents = null;
        private IGettableAsset _assetGetter = null;
        private List<IGameItem> _cachedItems = new List<IGameItem>();

        public GameItemsFactory(IGettableAsset assetGetter, ItemEvents itemEvents)
        {
            _assetGetter = assetGetter;
            _itemEvents = itemEvents;
        }

        public async Task<T> CreateGameItem<T>(string id, Vector3 position, bool cacheIt) where T : class, IGameItem
        {
            T unit = await _assetGetter.LoadResource<T>(id);

            unit.Init(position, _itemEvents);

            if (cacheIt)
                _cachedItems.Add(unit);

            return unit as T;
        }

        public void ClearAll()
        {
            foreach (var item in _cachedItems)
                item.Release();
            _cachedItems.Clear();
        }
    }
}