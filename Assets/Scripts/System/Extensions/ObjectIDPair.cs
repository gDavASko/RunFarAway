using UnityEngine;

namespace RFW
{
    [System.Serializable]
    public class ObjectIDPair<TKey, T> where TKey : IIDGetter
    {
        [SerializeField] private TKey id;
        public TKey ID => id;
        [SerializeField] private T obj;
        public T Obj => obj;

        public bool Check(TKey id)
        {
            return this.id.ID == id.ID;
        }
    }
}