using System;
using UnityEngine;
using RFW.Events;

namespace RFW
{
    public class GameController : IDisposable
    {
        private EventBinding<EventEndload> _eventLoadEnd = null;

        public GameController()
        {
            _eventLoadEnd = new EventBinding<EventEndload>(OnGameLoaded);
            EventBus<EventEndload>.Register(_eventLoadEnd);
        }

        private void OnGameLoaded()
        {
            Debug.LogError("GameLoaded");
        }

        public void Dispose()
        {
            EventBus<EventEndload>.Unregister(_eventLoadEnd);
        }
    }
}