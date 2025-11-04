using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem.Boss
{
    public class Summon : State
    {
        private Transform _body;
        private CharacterController _player;
        private AnimatorController _animator;
        private RotateToPlayer _rotor;
        private BossConfig _config;
        private List<Transform> _spawnPoints = new();
        private List<Transform> _enemies = new();
        private BossStates _states;
       // private Coroutine _coroutine;

        public Summon(
            IStateSwitcher stateSwitcher,
            Transform body,
            CharacterController player,
            BossConfig config,
            AnimatorController animator,
            RotateToPlayer rotor,
            List<Transform> spawnPoints,
            List<Transform> enemies,
            BossStates states
            ) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _animator = animator;
            _config = config;
            _rotor = rotor;
            _spawnPoints = spawnPoints;
            _enemies = enemies;
            _states = states;
        }

        public override void Start()
        {
            _animator.SetAttack(true);
            _rotor.enabled = true;
            DoSpawn();
        }

        public override void Stop()
        {
            _animator.SetAttack(false);
            _rotor.enabled = false;
        }

        public override void Update()
        {

        }

        private void DoSpawn()
        {
            if (_spawnPoints.Count < _enemies.Count)
            {
                Debug.LogError("Boss error _spawnPoints.Count != _enemies.Count");
                return;
            }
            for (var i = 0; i < _enemies.Count; i++)
            {
              var enemy = GameObject.Instantiate(_enemies[i], _spawnPoints[i].transform.position, Quaternion.identity);
                if (enemy.TryGetComponent(out Health health)) {
                    health.IsOver += _states.UnitIsKilled;
                }
                if (enemy.TryGetComponent(out StateBehaviour states))
                {
                    states.MaxAgro = true;
                }
            }
            _states.SetUnitToSpawn(_enemies.Count);
        }
    }
}