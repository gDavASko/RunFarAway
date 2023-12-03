using System.Collections.Generic;
using UnityEngine;

namespace KBP.CORE
{
    [System.Serializable]
    public class ObjectIdPairContainer<T>
    {
        [SerializeField]
        private List<ObjectIDPair<T>>   objectIds = null;

        private List<string> keys = null;
        public List<string> Keys
        {
            get
            {
                if(keys == null || Application.isEditor)
                {
                    keys = new List<string>();
                    foreach(var k in objectIds)
                    {
                        keys.Add(k.ID);
                    }
                }
                return keys;
            }
        }

        private List<T> values = null;
        public List<T> Values
        {
            get
            {
                if(values == null)
                {
                    values = new List<T>();
                    foreach(var v in ObjectsDict.Values)
                    {
                        values.Add(v);
                    }
                }

                return values;
            }
        }

        private Dictionary<string, T> objIdDict = null;
        private Dictionary<string, T> ObjectsDict
        {
            get
            {
                if(objIdDict == null)
                {
                    objIdDict = new Dictionary<string, T>();
                    foreach(ObjectIDPair<T> id in objectIds)
                    {
                        if(id != null)
                        {
                            objIdDict[id.ID] = id.Obj;
                        }
                    }
                }

                return objIdDict;
            }
        }

        private Dictionary<T, string> idObjectsDict = null;
        private Dictionary<T, string> IdDict
        {
            get
            {
                if(idObjectsDict == null)
                {
                    idObjectsDict = new Dictionary<T, string>();
                    foreach(ObjectIDPair<T> id in objectIds)
                    {
                        if(id != null)
                        {
                            idObjectsDict[id.Obj] = id.ID;
                        }
                    }
                }

                return idObjectsDict;
            }
        }

        public T GetObject(string id)
        {
            if(ObjectsDict.ContainsKey(id))
            {
                return ObjectsDict[id];
            }

            return default(T);
        }

        public string GetObjectID(T index)
        {
            if(IdDict.ContainsKey(index))
            {
                return IdDict[index];
            }
            return "";
        }

        public T this[string key]
        {
            get
            {
                return GetObject(key);
            }
        }

        public void Add(string key, T value)
        {
            if(objectIds == null)
            {
                objectIds = new List<ObjectIDPair<T>>();
            }
            var obj = new ObjectIDPair<T>(key, value);
            objectIds.Add(obj);
        }
    }
}