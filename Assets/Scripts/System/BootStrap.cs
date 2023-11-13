using RFW.Events;
using RFW.Levels;
using RFW.Saves;
using UnityEngine;

namespace RFW
{
    public class BootStrap : MonoBehaviour
    {
        private const string PLAYER_ID = "player";

        [SerializeField] private string[] _configsIds = null;

        private IConfigGetter _configs = null;

        private ILevelController _levelController = null;

        private IAssetGetter _assetGetter = null;

        private IPlayerFactory _playerFactory = null;
        private IItemFactory _itemsFactory = null;

        private ILevelFactory _levelFactory = null;

        private IStorableParams _saves = null;

        private GameController _gameController = null;
        private UnitsAccessor _unitAccessor = null;

        private UnitEvents _unitEvents = null;
        private ItemEvents _itemEvents = null;
        private GameEvents _gameEvents = null;

        private async void Awake()
        {
            _assetGetter = new AddressableCachedAssetGetter();

            SOConfigsContainer configs = new SOConfigsContainer();
            await configs.LoadCofigsAsync(_configsIds, new AddressableCommonAssetGetter());
            _configs = configs;


            _unitEvents = new UnitEvents();
            _itemEvents = new ItemEvents();
            _gameEvents = new GameEvents();

            _saves = new PrefsSaves();

            _playerFactory = new PlayerFactory(PLAYER_ID, _assetGetter, _unitEvents);
            _itemsFactory = new ItemsFactory(_assetGetter, _unitEvents);
            _levelFactory = new LevelFactory(_assetGetter);

            _unitAccessor = new UnitsAccessor(_playerFactory, _itemsFactory, _unitEvents, _itemEvents);
            _levelController = new EndlessLevelController(_levelFactory,
                _gameEvents, _unitEvents, _itemEvents, _configs);
            _gameController = new GameController(_gameEvents);

            _gameEvents.OnGameLoaded?.Invoke();
        }
    }
}