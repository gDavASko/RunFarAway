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

        private Transform _player = null;
        private Vector3 _velocity = default;
        private bool _isActiveCamera = false;

        private EventBinding<EventUnitCreated> _eventUnitCreated = null;
        private EventBinding<EventGameStart> _eventGameStart = null;
        private EventBinding<EventGameEnd> _eventGameEnd = null;
        

        public void Construct()
        {
            _eventUnitCreated = new EventBinding<EventUnitCreated>(OnUnitCreated);
            EventBus<EventUnitCreated>.Register(_eventUnitCreated);

            _eventGameStart = new EventBinding<EventGameStart>(OnGameStart);
            EventBus<EventGameStart>.Register(_eventGameStart);
            
            _eventGameEnd = new EventBinding<EventGameEnd>(OnGameFinish);
            EventBus<EventGameEnd>.Register(_eventGameEnd);
        }

        private void LateUpdate()
        {
            if (!_isActiveCamera || _player == null)
                return;

            Vector3 targetPosition = _player.transform.position + _cameraOffset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
        }

        private void OnUnitCreated(EventUnitCreated unit)
        {
            if (unit.UnitTransform.CompareTag("Player"))
            {
                _player = unit.UnitTransform;
            }
        }

        private void OnGameFinish()
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
            EventBus<EventUnitCreated>.Unregister(_eventUnitCreated);
            EventBus<EventGameStart>.Unregister(_eventGameStart);
            EventBus<EventGameEnd>.Unregister(_eventGameEnd);
        }
    }
}