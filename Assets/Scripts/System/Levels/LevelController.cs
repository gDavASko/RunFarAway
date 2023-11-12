using System.Collections.Generic;
using System.Threading.Tasks;
using RFW.Events;
using UnityEngine;

namespace RFW
{
    public class LevelController : MonoBehaviour, ILevelController
    {
        [SerializeField] private string[] _levelsOrderedIds = null;
        [SerializeField] private int _cycledFrom = 3;

        private IUnitsFactory _unitsFactory = null;
        private ILevelFactory _levelFactory = null;
        private IGameItemsFactory _itemsFactory = null;

        private GameEvents _gameEvents = null;
        private ILevel _currentLevel = null;


        private void Construct(IUnitsFactory unitsFactory, ILevelFactory levelFactory,
            IGameItemsFactory itemsFactory, GameEvents gameEvents)
        {
            _unitsFactory = unitsFactory;
            _levelFactory = levelFactory;
            _itemsFactory = itemsFactory;

            _gameEvents = gameEvents;
            gameEvents.OnNextGame += OnNextGame;
            gameEvents.OnRestartGame += OnRestartGame;

            LoadCurrent();
        }

        public ILevel CurrentLevel => _currentLevel;

        public void LoadNext()
        {
            LoadLevelByNumber(0);
        }

        public async void LoadCurrent()
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

            InitEnemy();
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
            foreach (KeyValuePair<string, Vector3> enemy in _currentLevel.ItemsSpawnPoints)
            {
                ConstructItem(enemy.Key, enemy.Value);
            }
        }

        private async Task<IGameItem> ConstructItem(string id, Vector3 pos)
        {
            IGameItem item = await _itemsFactory.CreateGameItem<IGameItem>(id, pos, true);
            return item;
        }

        private void InitEnemy()
        {
            foreach (KeyValuePair<string, Vector3> enemy in _currentLevel.EnemySpawnPoints)
            {
                ConstructUnit(enemy.Key, enemy.Value);
            }
        }

        public async Task<IUnit> ConstructUnit(string id, Vector3 pos)
        {
            IUnit unit = await _unitsFactory.CreateUnit<IUnit>(id, pos,
                Quaternion.identity, _currentLevel.Transform, true);

            return unit;
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