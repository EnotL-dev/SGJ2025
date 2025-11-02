using UnityEngine;

namespace EnemySystem.Worm
{
    public class Waiting : State
    {
        private Transform _body;
        private CharacterController _player;
        private float _distance;
        private WormConfig _config;

        public Waiting(IStateSwitcher stateSwitcher, Transform body, CharacterController player, WormConfig config) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _config = config;
        }

        public override void Start()
        {
        }

        public override void Stop()
        { 
        }

        public override void Update()
        {
            _distance = Vector3.Distance(_body.position, _player.transform.position);
            if (_distance < _config.DistanceDetect)
            {
                _stateSwitcher.SwitchState<Attack>();
            }
        }
    }
}