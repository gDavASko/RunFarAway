using UnityEngine;

namespace RFW
{
    [System.Serializable]
    public class StringID : IIDGetter
    {
        [SerializeField] private string id = "";
        public string ID => id;
    }
}