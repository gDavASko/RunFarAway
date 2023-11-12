using System.Threading.Tasks;
using UnityEngine;

namespace RFW
{
    public interface ILevelController
    {
        ILevel CurrentLevel { get; }


        void LoadNext();
        void LoadCurrent();
        Task<IUnit> ConstructUnit(string id, Vector3 pos);
    }
}