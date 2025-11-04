using PlayerSystem;
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
            _animator.SetAttack(true);
            _rotor.enabled = false;
            _startPosition = _body.position;
            _animator.StartCoroutine(DoAttack());
        }

        public override void Stop()
        {
            _animator.SetAttack(false);
            _rotor.enabled = true;
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

        private IEnumerator DoAttack()
        {
            yield return Move(PlayerRefs.Instance.Health.gameObject.transform.position, _config.AttackLunge.DistanceAfterPlayer);
            yield return Move(_startPosition, 0);
            _stateSwitcher.SwitchState<AttackSelect>();
        }

        private IEnumerator Move(Vector3 targetPosition, float additionalDistance)
        {
            Vector3 startPosition = _body.transform.position;
            Vector3 direction = targetPosition - _body.transform.position;
            direction.Normalize();

            float totalDistance = Vector3.Distance(targetPosition, _body.transform.position) + additionalDistance;

            float elapsedTime = 0f;
            _body.transform.position = startPosition;

            while (elapsedTime < _config.AttackLunge.Time)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / _config.AttackLunge.Time;
                 t = Mathf.SmoothStep(0f, 1f, t);
                _body.transform.position = Vector3.Lerp(startPosition, direction * totalDistance, t);
                yield return new WaitForEndOfFrame();
            }
            _body.transform.position = direction * totalDistance;
        }
    }
}