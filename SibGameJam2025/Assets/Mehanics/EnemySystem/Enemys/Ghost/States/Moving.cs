using UnityEngine;

namespace EnemySystem.Ghost
{
    public class Moving : State
    {
        private Transform _body;
        private CharacterController _player;
        private float _distance;
        private AnimatorController _animator;
        private GhostConfig _config;
        private float _distanceDetect;
        private RotateToPlayer _rotor;

        public Moving(IStateSwitcher stateSwitcher, Transform body, CharacterController player, GhostConfig config, AnimatorController animator, float distanceDetect, RotateToPlayer rotor) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _animator = animator;
            _config = config;
            _distanceDetect = distanceDetect;
            _rotor = rotor;
        }

        public override void Start()
        {
            _animator.SetWalk(true);
            _rotor.enabled = true;
        }

        public override void Stop()
        {
            _animator.SetWalk(false);
            _rotor.enabled = false;
        }

        public override void Update()
        {
            _distance = Vector3.Distance(_body.position, _player.transform.position);
            if (_distance > _distanceDetect)
            {
                _stateSwitcher.SwitchState<Waiting>();
            }
            if (_distance < _config.DistanceAttack)
            {
                _stateSwitcher.SwitchState<Attack>();
            }
            MoveToPlayer();
        }

        private void MoveToPlayer()
        {
            Vector3 direction = _player.transform.position - _body.transform.position;
            direction.Normalize();
            _body.transform.position += direction * _config.Speed * Time.deltaTime;
        }
    }
}