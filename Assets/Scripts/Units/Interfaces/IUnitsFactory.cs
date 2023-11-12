using Cysharp.Threading.Tasks;

namespace RFW
{
    public interface IUnitsFactory
    {
        UniTask<T> CreateUnitAsync<T>(string unitId, params object[] parameters)
            where T : class, IUnit;
    }
}