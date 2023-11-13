using UnityEngine;

namespace RFW
{
    public interface IAssetGetter : IGettableAsset, IUnloadableAsset
    {
        public GameObject CachedObject { get; }
    }
}