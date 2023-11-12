using System;
using System.Collections.Generic;
using UnityEngine;

namespace RFW
{
    public class UnitBase : IUnit
    {
        public IUnitView UnitView
        {
            get;
            private set;
        } = null;

        private List<IUnitSystem> _systems = null;
        private Dictionary<Type, IUnitSystem> _sysDictionary = null;


        public T GetSystem<T>() where T :class, IUnitSystem
        {
            if (_systems == null || !_sysDictionary.ContainsKey(typeof(T)))
            {
                Debug.LogError($"[{nameof(UnitBase)}] Try to get non exist system for {nameof(T)}");
                return null;
            }

            return _sysDictionary[typeof(T)] as T;
        }

        public void Init(params object[] parameters)
        {
            UnitView = parameters.Get<IUnitView>();

            _systems = parameters.Get<List<IUnitSystem>>();
            foreach (var system in _systems)
            {
                _sysDictionary[system.SystemType] = system;
            }

            Vector3 position = parameters.Get<Vector3>();
            UnitView.transform.position = position;
        }

        public void Dispose()
        {
            UnitView.Dispose();

            _systems.Clear();
            _systems = null;
        }
    }
}