using System;
using System.Collections.Generic;
using UnityEngine;

namespace RFW
{
    public class CurrentUnitContext: IDisposable
    {
        private IUnitView _unitView = null;

        private List<IUnitSystem> _systems = null;
        private Dictionary<Type, IUnitSystem> _sysDictionary = null;

        public CurrentUnitContext(IUnitView unit)
        {
            _unitView = unit;
        }

        public T GetSystem<T>() where T :class, IUnitSystem
        {
            if (_systems == null || !_sysDictionary.ContainsKey(typeof(T)))
            {
                Debug.LogError($"[{nameof(CurrentUnitContext)}] Try to get non exist " +
                               $"system of type {nameof(T)} " +
                               $"for unit <{_unitView.transform.name}>");
                return null;
            }

            return _sysDictionary[typeof(T)] as T;
        }

        public void AddSystem<T>(T system) where T :class, IUnitSystem
        {
            if (_sysDictionary == null)
                _sysDictionary = new Dictionary<Type, IUnitSystem>();

            _sysDictionary[system.SystemType] = system;
        }

        public void Dispose()
        {
            _unitView.Dispose();

            if (_sysDictionary != null)
            {
                foreach (IUnitSystem system in _sysDictionary.Values)
                {
                    system.Dispose();
                }

                _sysDictionary.Clear();
                _sysDictionary = null;
            }
        }
    }
}