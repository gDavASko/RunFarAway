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

        private async void Awake()
        {
            _assetGetter = new AddressableCachedAssetGetter();

            SOConfigsContainer configs = new SOConfigsContainer();
            await configs.LoadCofigsAsync(_configsIds, new AddressableCommonAssetGetter());
            _configs = configs;

            _cameraController = Camera.main.GetComponent<CameraController>();
            _cameraController.Construct();

            _saves = new PrefsSaves();

            _playerInput = new PCInput();
            _playerFactory = new PlayerFactory(PLAYER_ID, _assetGetter, _playerInput);
            _unitsFactory = new UnitsFactory(_assetGetter);
            _unitAccessor = new UnitsAccessor(_playerFactory, _unitsFactory);

            _levelFactory = new LevelFactory(_assetGetter);
            _levelController = new EndlessLevelController(_levelFactory, _configs);

            _gameController = new GameController();

            EventBus<EventEndload>.Raise(new EventEndload());
        }

        private void OnDestroy()
        {
            _cameraController = null;
            
            _unitAccessor.Dispose();
            _unitAccessor = null;
            _gameController.Dispose();
            _gameController = null;
            _saves.Dispose();
            _saves = null;
            _levelFactory.Dispose();
            _levelFactory = null;
            _unitsFactory.Dispose();
            _unitsFactory = null;
            _playerFactory.Dispose();
            _playerFactory = null;
            _playerInput.Dispose();
            _playerInput = null;
            _assetGetter.Dispose();
            _assetGetter = null;
            _levelController.Dispose();
            _levelController = null;
            _configs.Dispose();
            _configs = null;
        }
    }
}