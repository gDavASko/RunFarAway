using System.Collections.Generic;
using System.Threading.Tasks;
using RFW.Events;
using UnityEngine;

namespace RFW.Levels
{
    public class CycledLevelController : ILevelController
    {
        public ILevel CurrentLevel => _currentLevel;

        private string[] _levelsOrderedIds = null;
        private int _cycledFrom = 3;

        private ILevelFactory _levelFactory = null;
        private ILevel _currentLevel = null;

        private GameEvents _gameEvents = null;
        private UnitEvents _unitEvents = null;
        private ItemEvents _itemEvents = null;


        public CycledLevelController(ILevelFactory levelFactory,
            GameEvents gameEvents, UnitEvents unitEvents, ItemEvents itemEvents)
        {
            _levelFactory = levelFactory;

            _gameEvents = gameEvents;
            gameEvents.OnNextGame += OnNextGame;
            gameEvents.OnRestartGame += OnRestartGame;

            _unitEvents = unitEvents;
            _itemEvents = itemEvents;

            LoadCurrent();
        }

        public void LoadNext()
        {
            LoadLevelByNumber(0);
        }

        public void LoadCurrent()
        {
            LoadLevelByNumber(0);
        }

        private async void LoadLevelByNumber(int number)
        {
            if (_currentLevel != null)
                UnloadLevel();

            _currentLevel =
                await _levelFactory.CreateLevel<ILevel>(GetLevelId(number));
            _currentLevel.Init(_gameEvents);

            InitUnits();
            InitItems();
        }

        public int CyclicIndex(int curNumber)
        {
            if (curNumber > _levelsOrderedIds.Length - 1)
            {
                curNumber = _cycledFrom + (int)((curNumber - _cycledFrom) % (_levelsOrderedIds.Length - _cycledFrom));
            }

            return curNumber;
        }

        private string GetLevelId(int levelNumber)
        {
            int index = CyclicIndex(levelNumber);

            return _levelsOrderedIds[index];
        }

        private void InitItems()
        {
            foreach (KeyValuePair<string, Vector3> item in _currentLevel.ItemsSpawnPoints)
            {
                _itemEvents.OnItemCreateRequest?.Invoke(item.Key, item.Value);
            }
        }



        private void InitUnits()
        {
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

        // private async Task<IGameItem> ConstructItem(string id, Vector3 pos)
        // {
        //     IGameItem item = await _itemsFactory.CreateGameItem<IGameItem>(id, pos, true);
        //     return item;
        // }

        //public async Task<IUnit> ConstructUnit(string id, Vector3 pos)
        //{
        //    IUnit unit = await _unitsFactory.CreateUnit<IUnit>(id, pos,
        //        Quaternion.identity, _currentLevel.Transform, true);
        //
        //    return unit;
        //}
    }
}