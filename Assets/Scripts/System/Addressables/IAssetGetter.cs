
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public interface IAssetGetter : IGettableAsset, IUnloadableAsset
{
    public GameObject CachedObject { get; }
}

public interface IUnloadableAsset
{
    void UnloadResource();
}

public interface IGettableAsset
{
    Task<T> LoadResource<T>(string assetId);
}

public interface IAsyncGettableAsset<T>
{
    void LoadResource(string assetId, System.Action<T> loadCompleteCallback);
}