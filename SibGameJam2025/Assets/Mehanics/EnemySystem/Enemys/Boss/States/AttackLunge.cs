using System.Collections;
using UnityEngine;

namespace EnemySystem.Boss
{
    public class AttackLunge : State
    {
        private Transform _body;
        private CharacterController _player;
        private AnimatorController _animator;
        private RotateToPlayer _rotor;
        private BossConfig _config;
        private Vector3 _startPosition;
        private Vector3 _direction;
        private float _totalDistance;
        private bool _damaged = false;

        public AttackLunge(IStateSwitcher stateSwitcher, Transform body, CharacterController player, BossConfig config, AnimatorController animator, float distanceDetect, RotateToPlayer rotor) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _animator = animator;
            _config = config;
            _rotor = rotor;
        }

        public override void Start()
        {
            _animator.SetWalk(true);
            _rotor.enabled = true;
            

            _direction = _player.transform.position - _body.transform.position;
            _direction.Normalize();

            _totalDistance = Vector3.Distance(_player.transform.position, _body.transform.position) + _config.AttackLunge.DistanceAfterPlayer;
            _animator.StartCoroutine(Move());
        }

        public override void Stop()
        {
            _animator.SetWalk(false);
            _rotor.enabled = false;
        }

        public override void Update()
        {
            if (!_damaged && Vector3.Distance(_player.transform.position, _body.transform.position) < _config.AttackLunge.DistanceToDamage)
            {
                _damaged = true;
                if (_player.TryGetComponent(out Health health))
                {
                    health.Reduce(_config.AttackLunge.Damage);
                }
            }
        }

        private IEnumerator Move()
        {
            float elapsedTime = 0f;
            _body.transform.position = _startPosition;

            while (elapsedTime < _config.AttackLunge.Time)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / _config.AttackLunge.Time;
                t = Mathf.SmoothStep(0f, 1f, t);
                _body.transform.position = Vector3.Lerp(_startPosition, _direction * _totalDistance, t);
                yield return new WaitForEndOfFrame();
            }
            _body.transform.position = _direction * _totalDistance;
            _stateSwitcher.SwitchState<AttackSelect>();
        }
    }
}