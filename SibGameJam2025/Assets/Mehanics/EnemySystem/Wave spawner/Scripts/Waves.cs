using PlayerSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem {
    public class Waves : MonoBehaviour
    {
        public Action<int> EndOfWaveNumber { get; set; }
        public Action EndOfWaves {  get; set; }
        [SerializeField] private List<StateBehaviour> _startEnemies = new();
        [Space]
        [SerializeField] private List<EnemyWave> _waveEnemys = new();
        [SerializeField] private List<Transform> _spawnPoints = new();
        private int _countToEnd = 0;

        [Serializable]
        public class EnemyWave
        {
            public IReadOnlyList<GameObject> Prefabs => _prefabs;
            [SerializeField] private List<GameObject> _prefabs = new();
        }

        [ContextMenu("Land points")]
        public void LandPoints()
        {
            foreach (var point in _spawnPoints)
            {
                Vector3 position = point.transform.position;
                position.y += 1f;
                 if (Physics.Raycast(position, -Vector3.up, out RaycastHit hit))
                {
                    if (point.transform.position.y != hit.point.y)
                    {
                        point.transform.position = hit.point;
                        Debug.Log($"Point ({point}) moved down");
                    }
                }
                else
                {
                    Debug.LogError($"No collider under the _spawnPoints:({point})!!11!!111!");
                }
            }
        }

        private void Start()
        {
            ConnectToStartMobs();
            if (CheckCount())
            {
                PlayerRefs.Instance.levelManager.InitializeLevel(GetTotalEnemyCount());
                StartCoroutine(DoWaves());
            }
        }

        private int GetTotalEnemyCount()
        {
            int count = 0;
            for (var i = 0; i < _waveEnemys.Count; i++)
            {
                count += _waveEnemys[i].Prefabs.Count;
            }
            count += _startEnemies.Count;
            return count;
        }

        private void ConnectToStartMobs()
        {
            for (var i = 0; i < _startEnemies.Count; i++)
            {
                _startEnemies[i].Died += DecreaseCount;
            }
            _countToEnd = _startEnemies.Count;
        }

        private bool CheckCount()
        {
            for (var i = 0; i < _waveEnemys.Count; i++)
            {
                if (_waveEnemys[i].Prefabs.Count > _spawnPoints.Count)
                {
                    Debug.LogError("Error: count spawnPoints and enemy in a wave are not equal");
                    return false;
                }
            }
            return true;
        }

        private IEnumerator DoWaves()
        {
            yield return new WaitUntil(() => _countToEnd <= 0);
            for (var i = 0; i < _waveEnemys.Count; i++)
            {
                PlayerRefs.Instance.levelManager.NewWaweMessage();
                Debug.Log($"Start {i + 1} wave");
                SpawnMobs(_waveEnemys[i].Prefabs);
                yield return new WaitUntil(() => _countToEnd <= 0);
                EndOfWaveNumber?.Invoke(i + 1);
                Debug.Log($"End {i + 1} wave");
            }
            EndOfWaves?.Invoke();
        }

        private void DecreaseCount()
        {
            _countToEnd--;
        }

        private void SpawnMobs(IReadOnlyList<GameObject> mobs)
        {
            for (var i = 0; i < mobs.Count; i++)
            {
                if (mobs[i] == null)
                    return;
                var mob = Instantiate(mobs[i], _spawnPoints[i].position, Quaternion.identity);
                if (mob.TryGetComponent(out StateBehaviour states))
                {
                    states.Died += DecreaseCount;
                    states.MaxAgro = true;
                    _countToEnd++;
                }
                else
                {
                    Debug.LogError($"Error: No StateBehaviour on the enemy {mobs[i]}");
                }
            }
        }
    }
}