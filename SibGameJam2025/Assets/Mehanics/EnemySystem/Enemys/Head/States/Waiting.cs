using UnityEngine;

namespace EnemySystem.Head
{
    public class Waiting : State
    {
        private Transform _body;
        private CharacterController _player;
        private float _distance;
        private HeadConfig _config;
        private AnimatorController _animator;

        public Waiting(IStateSwitcher stateSwitcher, Transform body, CharacterController player, HeadConfig config, AnimatorController animator) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _config = config;
            _animator = animator;
        }

        public override void Start()
        {
            _animator.SetIdle(true);
        }

        public override void Stop()
        {
            _animator.SetIdle(false);
        }

        public override void Update()
        {
            _distance = Vector3.Distance(_body.position, _player.transform.position);
            if (_distance < _config.DistanceDetect)
            {
                _stateSwitcher.SwitchState<Moving>();
            }
        }
    }
}