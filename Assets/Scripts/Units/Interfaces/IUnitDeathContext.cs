using UnityEngine;

namespace RFW
{
    public interface IUnitDeathContext
    {
        string UnitId { get; }
        Vector3 Position { get; }
    }
}