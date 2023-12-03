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

        private readonly EventBinding<EventGameRestart> _eventGameRestart = null;
        private readonly EventBinding<EventGameLoadNext> _eventGameLoadNext = null;
        private readonly EventBinding<EventEndload> _eventGameLoaded = null;

        public EndlessLevelController(ILevelFactory levelFactory, IConfigGetter configs)
        {
            _levelFactory = levelFactory;

            _eventGameRestart = new EventBinding<EventGameRestart>(OnRestartGame);
            EventBus<EventGameRestart>.Register(_eventGameRestart);
            
            _eventGameLoadNext = new EventBinding<EventGameLoadNext>(OnNextGame);
            EventBus<EventGameLoadNext>.Register(_eventGameLoadNext);

            _eventGameLoaded = new EventBinding<EventEndload>(OnGameLoaded);
            EventBus<EventEndload>.Register(_eventGameLoaded);

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
            InitLevel(_currentLevel);

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
                InitLevel(_currentLevel);
            }

            InitUnits();
            InitItems();
        }

        private void InitLevel(ILevel level)
        {
            level.Init();
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
                EventBus<EventUnitCreateRequest>.Raise(new EventUnitCreateRequest(item.Key, item.Value));
            }
        }

        private void InitUnits()
        {
            if (_currentLevel == null || _currentLevel.EnemySpawnPoints == null)
                return;

            foreach (KeyValuePair<string, Vector3> unit in _currentLevel.EnemySpawnPoints)
            {
                EventBus<EventUnitCreateRequest>.Raise(new EventUnitCreateRequest(unit.Key, unit.Value));
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

        public void Dispose()
        {
            EventBus<EventEndload>.Unregister(_eventGameLoaded);
            EventBus<EventGameRestart>.Unregister(_eventGameRestart);
            EventBus<EventGameLoadNext>.Unregister(_eventGameLoadNext);
        }
    }
}