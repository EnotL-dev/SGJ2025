using BulletSystem;
using System.Collections;
using UnityEngine;

namespace EnemySystem.Worm
{
    public class Attack : State
    {
        private BulletPool _bulletPool;
        private Transform _body;
        private CharacterController _player;
        private float _distance;
        private WormConfig _config;
        private AnimatorController _animator;
        private Transform _launchPoint;
        private LayerMask _layerMask;
        private Coroutine _attackCoroutine;
        private AudioSource _shootSound;

        public Attack(
            IStateSwitcher stateSwitcher,
            BulletPool bulletPool,
            Transform body,
            CharacterController player,
            WormConfig config,
            AnimatorController animator,
            Transform launchPoint,
            LayerMask layerMask,
            AudioSource shootSound
            ) : base(stateSwitcher)
        {
            _bulletPool = bulletPool;
            _body = body;
            _player = player;
            _config = config;
            _animator = animator;
            _launchPoint = launchPoint;
            _layerMask = layerMask;
            _shootSound = shootSound;
        }

        public override void Start()
        {
            _animator.ChooseAttack(0);
            _animator.SetAnimationSpeed(_config.AttackAnimationSpeed);
            _attackCoroutine = _bulletPool.StartCoroutine(AttackProccess());
        }

        public override void Stop()
        {
            _animator.SetAnimationSpeed(1f);
            if (_attackCoroutine != null)
                _bulletPool.StopCoroutine(_attackCoroutine);
            _animator.SetAttack(false);
        }

        public override void Update()
        {
            CheckDistance();
        }

        private void CheckDistance()
        {
            _distance = Vector3.Distance(_body.position, _player.transform.position);
            if (_distance > _config.DistanceAttack + 0.2)
            {
                _stateSwitcher.SwitchState<Waiting>();
            }
        }

        private IEnumerator AttackProccess()
        {
            while (true)
            {
                if (!HaveObstacle())
                {
                    _animator.SetAttack(true);
                    yield return new WaitForSeconds(_config.AttackPrepareTime);
                    _bulletPool.Launch(_player.transform);
                    _shootSound.Play();
                    yield return new WaitForSeconds(_config.AttackTime);
                    _animator.SetAttack(false);
                    yield return new WaitForSeconds(_config.AttackFrequancy);
                }
                else
                {
                    yield return null;
                }
            }
        }

        private bool HaveObstacle()
        {
            Vector3 playerPos = _player.transform.position;
            playerPos.y += 1.5f;
            if (Physics.Raycast(_launchPoint.position, playerPos - _launchPoint.position, out RaycastHit hit, 1000, _layerMask))
            {
                if (!hit.collider.gameObject.TryGetComponent(out CharacterController player))
                {
                    return true;
                }
            }
            return false;
        }
    }
}