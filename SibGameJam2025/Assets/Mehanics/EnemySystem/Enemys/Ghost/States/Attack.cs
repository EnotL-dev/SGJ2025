using System.Collections;
using UnityEngine;


namespace EnemySystem.Ghost
{
    public class Attack : State
    {
        private Transform _body;
        private CharacterController _player;
        private float _distance;
        private AnimatorController _animator;
        private GhostConfig _config;
        private Health _playerHealth;
        private Coroutine _attackCoroutine;
        private RotateToPlayer _rotor;
        private AudioSource _attackSound;

        public Attack(
            IStateSwitcher stateSwitcher,
            Transform body,
            CharacterController player,
            GhostConfig config,
            AnimatorController animator,
            Health playerHealth,
            RotateToPlayer rotor,
            AudioSource attackSound
            ) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _animator = animator;
            _config = config;
            _playerHealth = playerHealth;
            _rotor = rotor;
            _attackSound = attackSound;
        }

        public override void Start()
        {
            _attackCoroutine = _animator.StartCoroutine(AttackProccess());
            _rotor.enabled = true;
        }

        public override void Stop()
        {
            if (_attackCoroutine != null)
                _animator.StopCoroutine(_attackCoroutine);
            _rotor.enabled = false;
            _animator.SetAttack(false);
        }

        public override void Update()
        {
            _distance = Vector3.Distance(_body.position, _player.transform.position);
            if (_distance > _config.DistanceAttack + 0.2)
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
                //    Debug.Log("damage");
                _attackSound.Play();
                yield return new WaitForSeconds(_config.AttackTime);
                _animator.SetAttack(false);
                yield return new WaitForSeconds(_config.AttackFrequency);
            }
        }
    }
}