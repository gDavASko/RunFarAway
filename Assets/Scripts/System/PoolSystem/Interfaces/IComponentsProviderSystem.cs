using UnityEngine;

namespace RFW.Pool
{
    public interface IComponentsProviderSystem
    {
        public void Register<poolType>(GameObject instanceGO)
            where poolType : MonoBehaviour, IPoolable<poolType>;

        poolType Get<poolType>()
            where poolType : MonoBehaviour, IPoolable<poolType>;

        IPooledComponentProvider<poolType> GetPoolSystem<poolType>()
            where poolType : MonoBehaviour, IPoolable<poolType>;
    }
}