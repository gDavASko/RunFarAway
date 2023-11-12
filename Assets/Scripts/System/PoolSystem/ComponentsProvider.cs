using System.Collections.Generic;
using UnityEngine;

namespace RFW.Pool
{
    public class ComponentsProvider<T>: IPooledComponentProvider<T>, IPoolSystem
        where T : MonoBehaviour, IPoolable<T>
    {
        private GameObject _instanceGO = null;
        private Stack<T> _poolList = null;

        public ComponentsProvider(GameObject poolObjectPrefab)
        {
            _instanceGO = poolObjectPrefab;
        }

        public T Get()
        {
            if(_poolList == null || _poolList.Count == 0)
            {
                return CreateNewComponent();
            }

            var component = _poolList.Pop();
            return component;
        }

        public void Release(T component)
        {
            if(_poolList != null && _poolList.Contains(component))
            {
                return;
            }

            if(_poolList == null)
            {
                _poolList = new Stack<T>();
            }

            _poolList.Push(component);
        }

        public void Dispose()
        {
            while(_poolList != null && _poolList.Count > 0)
            {
                var component = _poolList.Pop();
                if(component != null && component.gameObject != null)
                {
                    component.DestroySelf();
                }
            }
        }

        private T CreateNewComponent()
        {
            var instance =
                GameObject.Instantiate(_instanceGO, Vector3.zero, Quaternion.identity).GetComponent<T>();
            instance.SetReleaser(this);
            return instance;
        }
    }
}