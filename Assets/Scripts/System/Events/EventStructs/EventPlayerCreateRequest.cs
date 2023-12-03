using UnityEngine;

namespace RFW.Events
{
    public struct EventPlayerCreateRequest : IEvent
    {
        public static string UnitId => "player";
        public Vector3 Position;

        public EventPlayerCreateRequest(Vector3 position)
        {
            Position = position;
        }
    }
}