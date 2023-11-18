using System.Collections.Generic;
using RFW.Events;
using UnityEngine;

namespace RFW.Levels
{
    public class Level : MonoBehaviour, ILevel
    {
        [SerializeField] private string _id = "level";

        private LevelFinishTrigger _finishTrigger = null;
        private Transform _playerSpawn = null;
        private List<KeyValuePair<string, Vector3>> _enemiesSpawns = new List<KeyValuePair<string, Vector3>>();
        private List<KeyValuePair<string, Vector3>> _itemsSpawns = new List<KeyValuePair<string, Vector3>>();

        private GameEvents _gameEvents = null;
        private UnitEvents _unitEvents = null;

        public string Id => _id;
        public Transform Transform => transform;
        public Vector3 PlayerSpawnPoint => _playerSpawn.position;
        public List<KeyValuePair<string, Vector3>> EnemySpawnPoints => _enemiesSpawns;
        public List<KeyValuePair<string, Vector3>> ItemsSpawnPoints => _itemsSpawns;

        public void Init(params object[] parameters)
        {
            _gameEvents = parameters.Get<GameEvents>();
            _unitEvents = parameters.Get<UnitEvents>();

            InitObjects();
        }

        private void Awake()
        {
            {
                UnitMarker[] markers = transform.GetComponentsInChildren<UnitMarker>(false);

                foreach (var marker in markers)
                {
                    if (marker.Id != "Player")
                    {
                        _enemiesSpawns.Add(new KeyValuePair<string, Vector3>(marker.Id, marker.transform.position));
                    }
                    else
                    {
                        _playerSpawn = marker.transform;
                    }
                }
            }

            {
                GameItemMarker[] markers = transform.GetComponentsInChildren<GameItemMarker>(false);
                foreach (var marker in markers)
                {
                    _itemsSpawns.Add(new KeyValuePair<string, Vector3>(marker.Id, marker.transform.position));
                }
            }

            _finishTrigger = GetComponentInChildren<LevelFinishTrigger>();
            _finishTrigger.OnLevelFinish += FinishLevel;
        }

        public void Dispose()
        {
            Destroy(this.gameObject);
        }

        private void FinishLevel()
        {
            _gameEvents.OnGameFinish?.Invoke(GameEvents.GameResult.Victory);
        }

        private void InitObjects()
        {
            if (_playerSpawn != null)
            {
                _unitEvents.OnPlayerCreateRequest?.Invoke(_playerSpawn.transform.position);
            }

            foreach (var unit in _enemiesSpawns)
            {
                _unitEvents.OnUnitCreateRequest?.Invoke(unit.Key, unit.Value);
            }

            foreach (var item in _itemsSpawns)
            {
                _unitEvents.OnUnitCreateRequest?.Invoke(item.Key, item.Value);
            }
        }
    }
}