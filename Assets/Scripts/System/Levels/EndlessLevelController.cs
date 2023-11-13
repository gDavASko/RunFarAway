using System.Collections.Generic;
using RFW.Events;
using UnityEngine;

namespace RFW.Levels
{
    public class EndlessLevelController : ILevelController
    {
        public ILevel CurrentLevel => _currentLevel;

        private string[] _levelsOrderedIds = null;

        private ILevelFactory _levelFactory = null;
        private ILevel _currentLevel = null;

        private GameEvents _gameEvents = null;
        private UnitEvents _unitEvents = null;
        private ItemEvents _itemEvents = null;


        public EndlessLevelController(ILevelFactory levelFactory,
            GameEvents gameEvents, UnitEvents unitEvents, ItemEvents itemEvents, IConfigGetter configs)
        {
            _levelFactory = levelFactory;

            _gameEvents = gameEvents;
            gameEvents.OnNextGame += OnNextGame;
            gameEvents.OnRestartGame += OnRestartGame;
            gameEvents.OnGameLoaded += OnGameLoaded;

            _unitEvents = unitEvents;
            _itemEvents = itemEvents;

            _levelsOrderedIds = configs.GetConfig<SOLevelsList>().GetLevels();
        }

        private void OnGameLoaded()
        {
            LoadCurrent();
        }

        public async void LoadNext()
        {
            if (_currentLevel != null)
                UnloadLevel();

            _currentLevel = await _levelFactory.CreateLevel<ILevel>(GetNextLevelId());
            _currentLevel.Init(_gameEvents);

            InitUnits();
            InitItems();
        }

        public async void LoadCurrent()
        {
            if (_currentLevel == null)
            {
                LoadNext();
            }
            else
            {
                _currentLevel = await _levelFactory.CreateLevel<ILevel>(_currentLevel.Id);
                _currentLevel.Init(_gameEvents);
            }

            InitUnits();
            InitItems();
        }

        private string GetNextLevelId()
        {
            return _levelsOrderedIds.RandomElement();
        }

        private void InitItems()
        {
            if (_currentLevel == null || _currentLevel.ItemsSpawnPoints == null)
                return;

            foreach (KeyValuePair<string, Vector3> item in _currentLevel.ItemsSpawnPoints)
            {
                _itemEvents.OnItemCreateRequest?.Invoke(item.Key, item.Value);
            }
        }

        private void InitUnits()
        {
            if (_currentLevel == null || _currentLevel.EnemySpawnPoints == null)
                return;

            foreach (KeyValuePair<string, Vector3> unit in _currentLevel.EnemySpawnPoints)
            {
                _unitEvents.OnUnitCreateRequest?.Invoke(unit.Key, unit.Value);
            }
        }

        private void UnloadLevel()
        {
            _currentLevel.Dispose();
        }

        private void OnRestartGame()
        {
            LoadCurrent();
        }

        private void OnNextGame()
        {
            LoadNext();
        }
    }
}