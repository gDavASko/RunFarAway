using System;
using UnityEngine;

namespace RFW
{
    public interface IAssetGetter : IGettableAsset, IUnloadableAsset, IDisposable
    {
        public GameObject CachedObject { get; }
    }
}