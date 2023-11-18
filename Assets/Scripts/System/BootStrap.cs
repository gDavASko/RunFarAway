using RFW.Events;
using RFW.Levels;
using RFW.Saves;
using UnityEngine;

namespace RFW
{
    public class BootStrap : MonoBehaviour
    {
        private const string PLAYER_ID = "Player";

        [SerializeField] private string[] _configsIds = null;

        private IConfigGetter _configs = null;

        private ILevelController _levelController = null;

        private IAssetGetter _assetGetter = null;

        private IInput _playerInput = null;

        private IPlayerFactory _playerFactory = null;
        private IUnitsFactory _unitsFactory = null;

        private ILevelFactory _levelFactory = null;

        private IStorableParams _saves = null;

        private CameraController _cameraController = null;
        private GameController _gameController = null;
        private UnitsAccessor _unitAccessor = null;

        private UnitEvents _unitEvents = null;
        private GameEvents _gameEvents = null;

        private async void Awake()
        {
            _assetGetter = new AddressableCachedAssetGetter();

            SOConfigsContainer configs = new SOConfigsContainer();
            await configs.LoadCofigsAsync(_configsIds, new AddressableCommonAssetGetter());
            _configs = configs;

            _unitEvents = new UnitEvents();
            _gameEvents = new GameEvents();

            _cameraController = Camera.main.GetComponent<CameraController>();
            _cameraController.Construct(_unitEvents, _gameEvents);

            _saves = new PrefsSaves();

            _playerInput = new PCInput();
            _playerFactory = new PlayerFactory(PLAYER_ID, _assetGetter, _unitEvents, _playerInput);
            _unitsFactory = new UnitsFactory(_assetGetter);
            _unitAccessor = new UnitsAccessor(_playerFactory, _unitsFactory, _unitEvents);

            _levelFactory = new LevelFactory(_assetGetter);
            _levelController = new EndlessLevelController(_levelFactory,
                _gameEvents, _unitEvents, _configs);

            _gameController = new GameController(_gameEvents);

            _gameEvents.OnGameLoaded?.Invoke();
        }
    }
}