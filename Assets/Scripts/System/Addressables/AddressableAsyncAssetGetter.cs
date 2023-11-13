using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace RFW
{
    public class AddressableAsyncAssetGetter<T> : IAsyncGettableAsset<T> where T : class
    {
        private System.Action<T> _callback = null;

        public void LoadResource(string assetId, Action<T> loadCompleteCallback)
        {
            _callback = loadCompleteCallback;
            var assetRef = Addressables.LoadAssetAsync<T>(assetId);

            assetRef.Completed += OnLoadComplete;
        }

        private void OnLoadComplete(AsyncOperationHandle<T> obj)
        {
            _callback.Invoke(obj.Result as T);
        }
    }
}