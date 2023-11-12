using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RFW.Events;
using UnityEngine;

namespace RFW
{
    public class PlayerFactory : UnitsFactory, IPlayerFactory
    {
        private string _playerId = default;

        public PlayerFactory(string playerId, IGettableAsset assetGetter, UnitEvents unitEvents): base(assetGetter, unitEvents)
        {
            _playerId = playerId;
        }

        public async UniTask<UnitBase> CreatePlayerAsync(Vector3 position)
        {
            List<IUnitSystem> systems = new List<IUnitSystem>();

            systems.Add(new HitPointSystem());

            UnitBase unit = await CreateUnitAsync<UnitBase>(_playerId, systems, position);

            _unitEvents.OnUnitCreated?.Invoke(unit);
            return unit;
        }
    }
}