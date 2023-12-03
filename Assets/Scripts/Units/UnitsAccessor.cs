using System;
using Cysharp.Threading.Tasks;
using RFW.Events;

namespace RFW
{
    public class UnitsAccessor : IDisposable
    {
        private IPlayerFactory _playerFactory = null;
        private IUnitsFactory _unitsFactory = null;

        private EventBinding<EventUnitCreateRequest> _eventUnitCreateRequest = null;
        private EventBinding<EventPlayerCreateRequest> _eventPlayerCreateRequest = null;
        
        public UnitsAccessor(IPlayerFactory playerFactory, IUnitsFactory unitsFactory)
        {
            _playerFactory = playerFactory;
            _unitsFactory = unitsFactory;

            _eventUnitCreateRequest = new EventBinding<EventUnitCreateRequest>(OnUnitCreateRequest);
            EventBus<EventUnitCreateRequest>.Register(_eventUnitCreateRequest);
            
            _eventPlayerCreateRequest = new EventBinding<EventPlayerCreateRequest>(OnPlayerCreateRequest);
            EventBus<EventPlayerCreateRequest>.Register(_eventPlayerCreateRequest);
        }

        private void OnPlayerCreateRequest(EventPlayerCreateRequest eventData)
        {
            _playerFactory.CreatePlayerAsync(eventData.Position).Forget();
        }

        private void OnUnitCreateRequest(EventUnitCreateRequest eventData)
        {
            _unitsFactory.CreateUnitAsync<IUnitView>(eventData.UnitId, eventData.Position).Forget();
        }

        public void Dispose()
        {
            EventBus<EventUnitCreateRequest>.Unregister(_eventUnitCreateRequest);
            EventBus<EventPlayerCreateRequest>.Unregister(_eventPlayerCreateRequest);
            
            _playerFactory = null;
        }
    }
}