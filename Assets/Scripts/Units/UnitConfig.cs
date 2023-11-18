using UnityEngine;

namespace RFW
{
    [System.Serializable]
    public struct UnitConfig
    {
        [SerializeField] private string _id;
        [SerializeField, Range(0f, float.MaxValue)] private float _maxHP;
        [SerializeField, Range(0f, float.MaxValue)] private float _maxSpeed;
        [SerializeField, Range(0f, float.MaxValue)] private float _maxJump;
    }
}