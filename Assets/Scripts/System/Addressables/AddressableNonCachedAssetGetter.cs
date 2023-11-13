using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RFW
{
    public class AddressableNonCachedAssetGetter : IGettableAsset
    {
        public async UniTask<T> LoadResource<T>(string assetId)
        {
            var handle = Addressables.InstantiateAsync(assetId);
            var obj = await handle.Task;
            if (obj.TryGetComponent(out T componentObj) == false)
                Debug.LogError($"Try to get resourse with unknown asset id = {assetId}");
            return componentObj;
        }
    }
}