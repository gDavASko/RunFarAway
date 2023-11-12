using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using RFW.Events;
using UnityEngine;

namespace RFW
{
    public class UnitsAccessor : IDisposable
    {
        private UnitEvents _unitEvents = null;
        private ItemEvents _itemEvents = null;

        private IPlayerFactory _playerFactory = null;
        private IItemFactory _itemsFactory = null;

        public UnitsAccessor(IPlayerFactory playerFactory, IItemFactory itemsFactory,
            UnitEvents unitEvents, ItemEvents itemEvents)
        {
            _playerFactory = playerFactory;
            _itemsFactory = itemsFactory;

            _unitEvents = unitEvents;
            _unitEvents.OnPlayerCreateRequest += OnPlayerCreateRequest;

            _itemEvents = itemEvents;
            _itemEvents.OnItemCreateRequest += OnItemCreateRequest;
        }

        private void OnItemCreateRequest(string itemId, Vector3 position)
        {
            _itemsFactory.CreateItemAsync(itemId, position).Forget();
        }

        private void OnPlayerCreateRequest(Vector3 position)
        {
            _playerFactory.CreatePlayerAsync(position).Forget();
        }

        public void Dispose()
        {
            _unitEvents.OnPlayerCreateRequest -= OnPlayerCreateRequest;
            _unitEvents = null;

            _itemEvents.OnItemCreateRequest -= OnItemCreateRequest;
            _itemEvents = null;

            _playerFactory = null;
            _itemsFactory = null;
        }
    }
}