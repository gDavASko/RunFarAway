using RFW.Pool;
using UnityEngine;

namespace RFW
{
    public interface IUnitView: IPoolable<IUnitView>
    {
        Transform transform
        {
            get;
        }
    }
}