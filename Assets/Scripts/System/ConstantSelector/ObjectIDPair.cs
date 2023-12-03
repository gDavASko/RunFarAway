using UnityEngine;

namespace KBP.CORE
{

    [System.Serializable]
    public class ObjectIDPair<T>
    {
        [SerializeField]
        private string      id = "";
        public string ID => id;
        [SerializeField]
        private T         obj;
        public T Obj => obj;

        public bool Check(string id)
        {
            return this.id == id;
        }

        public ObjectIDPair(string key, T obj)
        {
            this.id = key;
            this.obj = obj;
        }

        public ObjectIDPair()
        {

        }
    }
}