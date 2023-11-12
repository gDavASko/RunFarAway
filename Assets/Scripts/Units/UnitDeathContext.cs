using UnityEngine;

namespace RFW
{
    public struct UnitDeathContext : IUnitDeathContext
    {
        public string UnitId { get; private set; }
        public Vector3 Position { get; private set; }

        public UnitDeathContext(string unitId, Vector3 position)
        {
            UnitId = unitId;
            Position = position;
        }
    }
}