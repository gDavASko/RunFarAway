using UnityEngine;

namespace RFW.Events
{
    public class UnitEvents
    {
        public System.Action<IUnitDeathContext> OnUnitDeath
        {
            get;
            set;
        }

        public System.Action<IUnit> OnUnitCreated
        {
            get;
            set;
        }

        public System.Action<string, Vector3> OnUnitCreateRequest
        {
            get;
            set;
        }

        public System.Action<Vector3> OnPlayerCreateRequest
        {
            get;
            set;
        }
    }
}