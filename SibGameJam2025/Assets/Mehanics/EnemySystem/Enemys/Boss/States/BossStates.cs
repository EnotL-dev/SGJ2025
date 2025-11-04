using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem.Boss
{
    public class BossStates : StateBehaviour
    {
        public int CountUnitsToSpawn => _countUnitsToSpawn;

        [SerializeField] private AnimatorController _animator;
        [SerializeField] private float _distanceDetect;
        [SerializeField] private RotateToPlayer _rotor;
        [SerializeField] private BossConfig _config;
        [SerializeField] private ExplosionTarget _explosionZone;
        [SerializeField] private Health _health;
        [SerializeField] private Collider _collider;
        [SerializeField] private float _timeToDestroy;
        [SerializeField] private List<Transform> _spawnPoints = new();
        [SerializeField] private List<Transform> _enemies = new();
        [SerializeField] private AudioSource _attackAudio;
        [SerializeField] private AudioSource _deathAudio;
        [SerializeField] private AudioSource _callAudio;
        private const int _attackCount = 1;
        private int _countUnitsToSpawn = 0;

        protected override void InitializeStates()
        {
            _states.Add(new Waiting(this, transform, PlayerRefs.Instance.CharacterController, _animator, _distanceDetect, _rotor));
            //_states.Add(new AttackLunge(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _distanceDetect, _rotor));
            _states.Add(new AttackExplosionOnPlayer(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _rotor, _explosionZone, _attackAudio));
            _states.Add(new Summon(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _rotor, _spawnPoints, _enemies, this, _callAudio));
            _states.Add(new AttackSelect(this, _animator, _rotor, _attackCount, _config, this));
            _states.Add(new Death(this, _animator, _timeToDestroy, transform));
            SwitchState<Waiting>();
        }

        protected override void OnAwake()
        {
            
        }

        public void UnitIsKilled()
        {
            _countUnitsToSpawn--;
        }

        public void SetUnitToSpawn(int count)
        {
            _countUnitsToSpawn = count;
        }

        private void OnEnable()
        {
            _health.IsOver += CallDeath;
        }

        private void OnDisable()
        {
            _health.IsOver -= CallDeath;
        }

        private void CallDeath()
        {
            SwitchState<Death>();
            _collider.enabled = false;
            _rotor.enabled = false;
            _explosionZone.gameObject.SetActive(false);
            Destroy(_explosionZone.gameObject);
            _deathAudio.Play();
            Died?.Invoke();
        }
    }
}