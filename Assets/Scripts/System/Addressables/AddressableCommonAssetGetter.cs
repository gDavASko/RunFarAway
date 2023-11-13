using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace RFW
{
    public class AddressableCommonAssetGetter: IGettableAsset
    {
        public async UniTask<T> LoadResource<T>(string assetId)
        {
            var assetRef = Addressables.LoadAssetAsync<T>(assetId);
            await assetRef.Task;
            return assetRef.Result;
        }
    }
}