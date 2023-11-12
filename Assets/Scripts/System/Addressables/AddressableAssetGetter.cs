using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableAssetGetter : AddressableNonCachedAssetGetter, IAssetGetter
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
}