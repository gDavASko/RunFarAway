using Cysharp.Threading.Tasks;
using RFW.Events;
using UnityEngine;

namespace RFW
{
    public class PlayerFactory : UnitsFactory, IPlayerFactory
    {
        private string _playerId = "Player";
        private IInput _playerInput = null;

        private CurrentUnitContext _curPlayer = null;

        public PlayerFactory(string playerId, IGettableAsset assetGetter, IInput input): base(assetGetter)
        {
            _playerId = playerId;
            _playerInput = input;
        }

        public async UniTask<IUnitView> CreatePlayerAsync(Vector3 position)
        {
            if (_curPlayer != null)
                _curPlayer.Dispose();

            IUnitView unit = await CreateUnitAsync<IUnitView>(_playerId, position);

            _curPlayer = new CurrentUnitContext(unit);

            HitPointSystem hpSystem = new HitPointSystem();
            hpSystem.Init(unit);
            _curPlayer.AddSystem(hpSystem);

            UnitMoveController moveController = new UnitMoveController();
            moveController.Init(unit, _playerInput);
            _curPlayer.AddSystem(moveController);

            EventBus<EventUnitCreated>.Raise(new EventUnitCreated(unit.transform));
            return unit;
        }

        public override void Dispose()
        {
            _playerInput = null;
            
            _curPlayer?.Dispose();
            _curPlayer = null;
            
            base.Dispose();
        }
    }
}