using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RFW
{
    public class AddressableCachedAssetGetter : AddressableNonCachedAssetGetter, IAssetGetter
    {
        private GameObject _cachedObj = null;

        public GameObject CachedObject => _cachedObj;

        public void UnloadResource()
        {
            if (_cachedObj == null)
                return;

            _cachedObj.SetActive(false);
            Addressables.ReleaseInstance(_cachedObj);
            _cachedObj = null;
        }
        
        public void Dispose()
        {
            _cachedObj = null;
        }
    }
}