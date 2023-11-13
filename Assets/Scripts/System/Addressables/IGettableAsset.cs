using Cysharp.Threading.Tasks;

namespace RFW
{
    public interface IGettableAsset
    {
        UniTask<T> LoadResource<T>(string assetId);
    }
}