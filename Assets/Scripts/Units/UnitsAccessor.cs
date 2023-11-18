using System;
using Cysharp.Threading.Tasks;
using RFW.Events;
using UnityEngine;

namespace RFW
{
    public class UnitsAccessor : IDisposable
    {
        private UnitEvents _unitEvents = null;

        private IPlayerFactory _playerFactory = null;
        private IUnitsFactory _unitsFactory = null;

        public UnitsAccessor(IPlayerFactory playerFactory, IUnitsFactory unitsFactory, UnitEvents unitEvents)
        {
            _playerFactory = playerFactory;
            _unitsFactory = unitsFactory;

            _unitEvents = unitEvents;
            _unitEvents.OnPlayerCreateRequest += OnPlayerCreateRequest;
            _unitEvents.OnUnitCreateRequest += OnUnitCreateRequest;
        }

        private void OnPlayerCreateRequest(Vector3 position)
        {
            _playerFactory.CreatePlayerAsync(position).Forget();
        }

        private void OnUnitCreateRequest(string unitId, Vector3 position)
        {
            _unitsFactory.CreateUnitAsync<IUnitView>(unitId, position).Forget();
        }

        public void Dispose()
        {
            _unitEvents.OnPlayerCreateRequest -= OnPlayerCreateRequest;
            _unitEvents = null;

            _playerFactory = null;
        }
    }
}