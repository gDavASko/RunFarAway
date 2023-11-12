using System.Collections.Generic;
using System.Threading.Tasks;
using RFW.Events;

namespace RFW
{
    public class PlayerFactory : UnitsFactory
    {
        private string _playerId = default;

        public PlayerFactory(string playerId, IGettableAsset assetGetter, UnitEvents unitEvents): base(assetGetter, unitEvents)
        {
            _playerId = playerId;
        }

        public async Task<UnitBase> CreatePlayer()
        {
            List<IUnitSystem> systems = new List<IUnitSystem>();

            systems.Add(new HitPointSystem());

            UnitBase unit = await CreateUnit<UnitBase>(_playerId, systems);
            return unit;
        }
    }
}