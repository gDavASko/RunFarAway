using RFW.Events;
using UnityEngine;

namespace RFW
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour, ICameraController
    {
        [SerializeField, Range(0f, 1f)] private float _smoothTime = 0.5f;
        [SerializeField] private Vector3 _cameraOffset = default;
        [SerializeField] private Vector3 _initPosition = default;

        private UnitEvents _unitEvents = null;
        private GameEvents _gameEvents = null;
        private IUnit _player = null;
        private Vector3 _velocity = default;
        private bool _isActiveCamera = false;

        private void Construct(UnitEvents unitEvents, GameEvents gameEvents)
        {
            _unitEvents = unitEvents;
            _unitEvents.OnUnitCreated += OnUnitCreated;

            _gameEvents = gameEvents;
            _gameEvents.OnGameStart += OnGameStart;
            _gameEvents.OnGameFinish += OnGameFinish;
        }

        private void LateUpdate()
        {
            if (!_isActiveCamera || _player == null)
                return;

            Vector3 targetPosition = _player.UnitView.transform.position + _cameraOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        }

        private void OnUnitCreated(IUnit unit)
        {
            if (unit.UnitView.transform.CompareTag("player"))
            {
                _player = unit;
            }
        }

        private void OnGameFinish(GameEvents.GameResult obj)
        {
            _isActiveCamera = false;
        }

        private void OnGameStart()
        {
            transform.position = _initPosition;
            _isActiveCamera = true;
        }


        public void Dispose()
        {
            _unitEvents.OnUnitCreated -= OnUnitCreated;
            _unitEvents = null;

            _gameEvents.OnGameStart -= OnGameStart;
            _gameEvents.OnGameFinish -= OnGameFinish;
            _gameEvents = null;
        }
    }
}