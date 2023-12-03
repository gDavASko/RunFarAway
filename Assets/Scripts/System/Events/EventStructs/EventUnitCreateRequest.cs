using UnityEngine;

namespace RFW.Events
{
    public struct EventUnitCreateRequest : IEvent
    {
        public string UnitId;
        public Vector3 Position;

        public EventUnitCreateRequest(string unitId, Vector3 position)
        {
            UnitId = unitId;
            Position = position;
        }
    }
}