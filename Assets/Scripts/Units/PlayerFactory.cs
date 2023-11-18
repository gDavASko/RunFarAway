using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using RFW.Events;
using UnityEngine;

namespace RFW
{
    public class PlayerFactory : UnitsFactory, IPlayerFactory
    {
        private string _playerId = "Player";
        private UnitEvents _unitEvents = null;
        private IInput _playerInput = null;

        private CurrentUnitContext _curPlayer = null;

        public PlayerFactory(string playerId, IGettableAsset assetGetter, UnitEvents unitEvents, IInput input): base(assetGetter)
        {
            _playerId = playerId;
            _unitEvents = unitEvents;
            _playerInput = input;
        }

        public async UniTask<IUnitView> CreatePlayerAsync(Vector3 position)
        {
            if (_curPlayer != null)
                _curPlayer.Dispose();

            IUnitView unit = await CreateUnitAsync<IUnitView>(_playerId, position);

            _curPlayer = new CurrentUnitContext(unit);

            HitPointSystem hpSystem = new HitPointSystem();
            hpSystem.Init(unit, _unitEvents);
            _curPlayer.AddSystem(hpSystem);

            UnitMoveController moveController = new UnitMoveController();
            moveController.Init(unit, _playerInput, _unitEvents);
            _curPlayer.AddSystem(moveController);

            _unitEvents.OnUnitCreated?.Invoke(unit);
            return unit;
        }
    }

    public class CurrentUnitContext: IDisposable
    {
        private IUnitView _unitView = null;

        private List<IUnitSystem> _systems = null;
        private Dictionary<Type, IUnitSystem> _sysDictionary = null;

        public CurrentUnitContext(IUnitView unit)
        {
            _unitView = unit;
        }

        public T GetSystem<T>() where T :class, IUnitSystem
        {
            if (_systems == null || !_sysDictionary.ContainsKey(typeof(T)))
            {
                Debug.LogError($"[{nameof(CurrentUnitContext)}] Try to get non exist " +
                               $"system of type {nameof(T)} " +
                               $"for unit <{_unitView.transform.name}>");
                return null;
            }

            return _sysDictionary[typeof(T)] as T;
        }

        public void AddSystem<T>(T system) where T :class, IUnitSystem
        {
            if (_sysDictionary == null)
                _sysDictionary = new Dictionary<Type, IUnitSystem>();

            _sysDictionary[system.SystemType] = system;
        }

        public void Dispose()
        {
            _unitView.Dispose();

            if (_sysDictionary != null)
            {
                foreach (IUnitSystem system in _sysDictionary.Values)
                {
                    system.Dispose();
                }

                _sysDictionary.Clear();
                _sysDictionary = null;
            }
        }
    }
}