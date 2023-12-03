using System;
using Cysharp.Threading.Tasks;

namespace RFW.Levels
{
    public interface ILevelFactory: IDisposable
    {
        public UniTask<T> CreateLevel<T>(string id) where T : class, ILevel;
    }
}