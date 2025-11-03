using UnityEngine;

namespace EnemySystem.Ghost
{
    public class Recoil : State
    {
        private Transform _body;
        private CharacterController _player;
        private float _distance;
        private AnimatorController _animator;
        private GhostConfig _config;
        private RotateToPlayer _rotor;
        private Vector3 _startPosition;
        private Vector3 _direction;

        public Recoil(IStateSwitcher stateSwitcher,
            Transform body,
            CharacterController player,
            GhostConfig config,
            AnimatorController animator,
            RotateToPlayer rotor
            ) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _animator = animator;
            _config = config;
            _rotor = rotor;
        }

        public override void Start()
        {
            _animator.SetIdle(true);
            _rotor.enabled = false;
            _startPosition = _body.position;
            _direction = _player.transform.position - _body.transform.position;
            _direction.Normalize();
        }

        public override void Stop()
        {
            _animator.SetIdle(false);
            _rotor.enabled = true;
        }

        public override void Update()
        {
            _distance = Vector3.Distance(_body.position, _startPosition);
            if (_config.Recoil.DistanceToRecoil - _distance < 0.5f)
            {
                _stateSwitcher.SwitchState<Moving>();
            }
            MoveBack();
        }

        private void MoveBack()
        {
            _body.transform.position += _direction * -_config.Speed * _config.Recoil.RecoilAcceleration.Evaluate(_distance / _config.Recoil.DistanceToRecoil) * Time.deltaTime;
        }
    }
}