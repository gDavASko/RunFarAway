using RFW.Events;
using UnityEngine;

namespace RFW
{
    public interface IGameItem
    {
        bool IsWeapon { get; }
        float Count { get; }

        Transform transform { get; }

        string id { get; }

        void Hide();
        void Init(Vector3 initPosition, ItemEvents itemEvents);
        void Release();
    }
}