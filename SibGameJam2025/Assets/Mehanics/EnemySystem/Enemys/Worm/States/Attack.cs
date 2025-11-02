using BulletSystem;
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
        private float _currentTimeBeetwenAttack;
        private float _currentTimeAttack;
        private AnimatorController _animator;
        private bool _attackProcess = false;
        private Transform _launchPoint;

        public Attack(IStateSwitcher stateSwitcher, BulletPool bulletPool, Transform body, CharacterController player, WormConfig config, AnimatorController animator, Transform launchPoint) : base(stateSwitcher)
        {
            _bulletPool = bulletPool;
            _body = body;
            _player = player;
            _config = config;
            _animator = animator;
            _launchPoint = launchPoint;
        }

        public override void Start()
        {
            _currentTimeBeetwenAttack = 0;
            _currentTimeAttack = 0;
            _animator.ChooseAttack(0);
            _animator.SetAnimationSpeed(_config.AttackAnimationSpeed);
        }

        public override void Stop()
        {
            _animator.SetAnimationSpeed(1f);
        }

        public override void Update()
        {
            CheckDistance();
            if (!HaveObstacle())
                LaunchBullet();
        }

        private void CheckDistance()
        {
            _distance = Vector3.Distance(_body.position, _player.transform.position);
            if (_distance > _config.DistanceAttack)
            {
                _stateSwitcher.SwitchState<Waiting>();
            }
        }

        private void LaunchBullet()
        {
            if (!_attackProcess)
            {
                if (_currentTimeBeetwenAttack > 0)
                {
                    _currentTimeBeetwenAttack -= Time.deltaTime;
                }
                else
                {
                    _currentTimeAttack = _config.AttackTime;
                    _attackProcess = true;
                    _bulletPool.Launch(_player.transform);
                    _currentTimeBeetwenAttack = _config.AttackFrequancy;
                    _animator.SetAttack(true);
                }
            }
            else
            {
                if (_currentTimeAttack > 0)
                {
                    _currentTimeAttack -= Time.deltaTime;
                }
                else
                {
                    _attackProcess = false;
                    _animator.SetAttack(false);
                }
            }
        }

        private bool HaveObstacle()
        {
            Vector3 playerPos = _player.transform.position;
            playerPos.y += 1.5f;
            if (Physics.Raycast(_launchPoint.position, playerPos - _launchPoint.position, out RaycastHit hit, 1000))
            {
                if (!hit.collider.gameObject.TryGetComponent(out CharacterController player))
                {
                    Debug.Log(hit.collider.gameObject);
                    return true;
                }
            }
            return false;
        }
    }
}