using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableNonCachedAssetGetter : IGettableAsset
{
    public async Task<T> LoadResource<T>(string assetId)
    {
        var handle = Addressables.InstantiateAsync(assetId);
        var obj = await handle.Task;
        if(obj.TryGetComponent(out T componentObj) == false)
            Debug.LogError($"Try to get resourse with unknown asset id = {assetId}");
        return componentObj;
    }
}