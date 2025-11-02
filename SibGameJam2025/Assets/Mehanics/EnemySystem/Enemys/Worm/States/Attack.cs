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
        private Transform _launchPoint;
        private AnimatorController _animator;

        public Attack(IStateSwitcher stateSwitcher, BulletPool bulletPool, Transform body, CharacterController player, WormConfig config, Transform launchPoint, AnimatorController animator) : base(stateSwitcher)
        {
            _bulletPool = bulletPool;
            _body = body;
            _player = player;
            _config = config;
            _launchPoint = launchPoint;
            _animator = animator;
        }

        public override void Start()
        {
            _currentTimeBeetwenAttack = 0;

        }

        public override void Stop()
        {
            
        }

        public override void Update()
        {
            CheckDistance();
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
            if (_currentTimeBeetwenAttack > 0)
            {
                _currentTimeBeetwenAttack -= Time.deltaTime;
            }
            else
            {
               
                _bulletPool.Launch(_player.transform);
                _currentTimeBeetwenAttack = _config.AttackFrequancy;
            }
        }
    }
}