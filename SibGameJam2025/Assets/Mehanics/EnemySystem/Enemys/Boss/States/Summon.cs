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
        private Coroutine _coroutine;
        private AudioSource _audioSource;

        public Summon(
            IStateSwitcher stateSwitcher,
            Transform body,
            CharacterController player,
            BossConfig config,
            AnimatorController animator,
            RotateToPlayer rotor,
            List<Transform> spawnPoints,
            List<Transform> enemies,
            BossStates states,
            AudioSource audioSource
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
            _audioSource = audioSource;
        }

        public override void Start()
        {
            _animator.ChooseAttack(1);
            _animator.SetAttack(true);
            _rotor.enabled = true;
            _coroutine = _animator.StartCoroutine(Spawn());
        }

        public override void Stop()
        {
            _animator.SetAttack(false);
            _rotor.enabled = false;
            _animator.StopCoroutine(_coroutine);
        }

        public override void Update()
        {

        }

        private IEnumerator Spawn()
        {
            yield return new WaitForSeconds(_config.Summon.TimeBeforeSummon);
            if (_spawnPoints.Count < _enemies.Count)
            {
                Debug.LogError("Boss error _spawnPoints.Count != _enemies.Count");
            }
            else
            {
                _audioSource.Play();
                for (var i = 0; i < _enemies.Count; i++)
                {
                    var enemy = GameObject.Instantiate(_enemies[i], _spawnPoints[i].transform.position, Quaternion.identity);
                    if (enemy.TryGetComponent(out Health health))
                    {
                        health.IsOver += _states.UnitIsKilled;
                    }
                    if (enemy.TryGetComponent(out StateBehaviour states))
                    {
                        states.MaxAgro = true;
                    }
                    yield return new WaitForSeconds(_config.Summon.TimeBetweeenSummon);
                }
                _states.SetUnitToSpawn(_enemies.Count);
            }
            _stateSwitcher.SwitchState<AttackSelect>();
        }
    }
}