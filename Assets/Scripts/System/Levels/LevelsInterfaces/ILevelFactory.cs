using System.Threading.Tasks;

namespace RFW.Levels
{
    public interface ILevelFactory
    {
        public Task<T> CreateLevel<T>(string id) where T : class, ILevel;
    }
}