using UnityEngine;

namespace EnemySystem.Ghost
{
    public class Waiting : State
    {
        private Transform _body;
        private CharacterController _player;
        private float _distance;
        private AnimatorController _animator;
        private float _distanceDetect;
        private RotateToPlayer _rotor;

        public Waiting(IStateSwitcher stateSwitcher, Transform body, CharacterController player, AnimatorController animator, float distanceDetect, RotateToPlayer rotor) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _animator = animator;
            _distanceDetect = distanceDetect;
            _rotor = rotor;
        }

        public override void Start()
        {
            _animator.SetIdle(true);
            _rotor.enabled = false;
        }

        public override void Stop()
        {
            _animator.SetIdle(false);
            _rotor.enabled = true;
        }

        public override void Update()
        {
            _distance = Vector3.Distance(_body.position, _player.transform.position);
            if (_distance < _distanceDetect)
            {
                _stateSwitcher.SwitchState<Moving>();
            }
        }
    }
}