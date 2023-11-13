using Cysharp.Threading.Tasks;

namespace RFW.Levels
{
    public class LevelFactory : ILevelFactory
    {
        private IAssetGetter _assetGetter = null;
        private ILevel curLevel = null;

        public LevelFactory(IAssetGetter assetGetter)
        {
            _assetGetter = assetGetter;
        }

        public async UniTask<T> CreateLevel<T>(string id) where T : class, ILevel
        {
            if (curLevel != null)
                _assetGetter.UnloadResource();

            curLevel = await _assetGetter.LoadResource<T>(id);
            return curLevel as T;
        }
    }
}