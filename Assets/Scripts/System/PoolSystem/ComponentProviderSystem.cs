using System;
using System.Collections.Generic;
using UnityEngine;

namespace RFW.Pool
{
    public class ComponentProviderSystem: IComponentsProviderSystem, IDisposable
    {
        private Dictionary<Type, IPoolSystem> _pools = new Dictionary<Type, IPoolSystem>();

        public void Register<poolType>(GameObject instanceGO)
            where poolType : MonoBehaviour, IPoolable<poolType>
        {
            if(_pools.TryGetValue(typeof(poolType), out IPoolSystem pool))
            {
                pool.Dispose();
            }

            _pools[typeof(poolType)] = new ComponentsProvider<poolType>(instanceGO);
        }

        public poolType Get<poolType>()
            where poolType : MonoBehaviour, IPoolable<poolType>
        {
            if(_pools.TryGetValue(typeof(poolType), out IPoolSystem poolSystem))
            {
                var pool = poolSystem as ComponentsProvider<poolType>;
                return pool.Get() as poolType;
            }

            Debug.LogError($"[{nameof(ComponentProviderSystem)}] " +
                $"Try to get not registered type <{typeof(poolType).FullName}> from pool!");
            return null;
        }

        public IPooledComponentProvider<poolType> GetPoolSystem<poolType>()
            where poolType : MonoBehaviour, IPoolable<poolType>
        {
            if(_pools.TryGetValue(typeof(poolType), out IPoolSystem poolSystem))
            {
                return poolSystem as ComponentsProvider<poolType>;
            }

            Debug.LogError($"[{nameof(ComponentProviderSystem)}] " +
                $"Try to get non exist pool type <{typeof(poolType).FullName}>!");
            return null;
        }

        public void Dispose()
        {
            _pools.Clear();
        }
    }
}