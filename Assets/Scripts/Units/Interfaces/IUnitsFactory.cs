using System.Threading.Tasks;
using UnityEngine;

namespace RFW
{
    public interface IUnitsFactory
    {
        Task<T> CreateUnit<T>(string unitId, params object[] parameters)
            where T : class, IUnit;
    }
}