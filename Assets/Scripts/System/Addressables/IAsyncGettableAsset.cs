using System;
using System.Threading.Tasks;

namespace RFW
{
    public interface IAsyncGettableAsset<T>
    {
        void LoadResource(string assetId, Action<T> loadCompleteCallback);
    }
}