using System.Collections;
using UnityEngine;

namespace EnemySystem.Head
{
    public class Attack : State
    {
        private Transform _body;
        private CharacterController _player;
        private float _distance;
        private HeadConfig _config;
        private AnimatorController _animator;
        private Health _playerHealth;
        private Coroutine _attackCoroutine;

        public Attack(IStateSwitcher stateSwitcher, Transform body, CharacterController player, HeadConfig config, AnimatorController animator, Health playerHealth) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _config = config;
            _animator = animator;
            _playerHealth = playerHealth;
        }

        public override void Start()
        {
            _attackCoroutine = _animator.StartCoroutine(AttackProccess());
        }

        public override void Stop()
        {
            if (_attackCoroutine != null)
                _animator.StopCoroutine(_attackCoroutine);
        }

        public override void Update()
        {
            _distance = Vector3.Distance(_body.position, _player.transform.position);
            if (_distance > _config.DistanceAttack)
            {
                _stateSwitcher.SwitchState<Moving>();
            }
        }

        private IEnumerator AttackProccess()
        {
            while (true)
            {
                _animator.SetAttack(true);
                yield return new WaitForSeconds(_config.AttackPrepareTime);
                _playerHealth.Reduce(_config.Damage);
                yield return new WaitForSeconds(_config.AttackTime);
                _animator.SetAttack(false);
                yield return new WaitForSeconds(_config.AttackFrequency);
            }
        }
    }
}