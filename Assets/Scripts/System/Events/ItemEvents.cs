using UnityEngine;

namespace RFW.Events
{
    public class ItemEvents
    {
        public System.Action<IGameItem> OnItemPlayerCollision
        {
            get;
            set;
        }

        public System.Action<string, Vector3> OnItemCreateRequest
        {
            get;
            set;
        }
    }
}