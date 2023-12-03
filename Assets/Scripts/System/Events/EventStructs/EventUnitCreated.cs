using UnityEngine;

namespace RFW.Events
{
    public struct EventUnitCreated: IEvent
    {
        public Transform UnitTransform;

        public EventUnitCreated(Transform transform)
        {
            UnitTransform = transform;
        }
    }
}