using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
        private NavMeshAgent _navMeshAgent;
        private AudioSource _attackAudio;

        public Attack(IStateSwitcher stateSwitcher, Transform body, CharacterController player, HeadConfig config, AnimatorController animator, Health playerHealth, NavMeshAgent navMeshAgent, AudioSource attackAudio) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _config = config;
            _animator = animator;
            _playerHealth = playerHealth;
            _navMeshAgent = navMeshAgent;
            _attackAudio = attackAudio;
        }

        public override void Start()
        {
           // _navMeshAgent.enabled = false;
            _attackCoroutine = _animator.StartCoroutine(AttackProccess());
        }

        public override void Stop()
        {
           // _navMeshAgent.enabled = true;
            if (_attackCoroutine != null)
                _animator.StopCoroutine(_attackCoroutine);
            _animator.SetAttack(false);
        }

        public override void Update()
        {
            _distance = Vector3.Distance(_body.position, _player.transform.position);
            if (_distance > _config.DistanceAttack + 0.2 && !PlayerAboveMe(_body))
            {
                _stateSwitcher.SwitchState<Moving>();
            }
        }

        private IEnumerator AttackProccess()
        {
            while (true)
            {
                _animator.SetAttack(true);
                _attackAudio.Play();
                yield return new WaitForSeconds(_config.AttackPrepareTime);
                _playerHealth.Reduce(_config.Damage);
                yield return new WaitForSeconds(_config.AttackTime);
                _animator.SetAttack(false);
                yield return new WaitForSeconds(_config.AttackFrequency);
            }
        }
    }
}