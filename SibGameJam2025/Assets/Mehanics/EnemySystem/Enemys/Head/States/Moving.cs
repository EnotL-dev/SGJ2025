using PlayerSystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Head
{
    public class Moving : State
    {
        private Transform _body;
        private CharacterController _player;
        private float _distance;
        private HeadConfig _config;
        private AnimatorController _animator;
        private NavMeshAgent _agent;
        private float _distanceDetect;

        public Moving(IStateSwitcher stateSwitcher, Transform body, CharacterController player, HeadConfig config, AnimatorController animator, NavMeshAgent agent, float distanceDetect) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _config = config;
            _animator = animator;
            _agent = agent;
            _distanceDetect = distanceDetect;
        }

        public override void Start()
        {
            _animator.SetWalk(true);
            _agent.isStopped = false;
        }

        public override void Stop()
        {
            _animator.SetWalk(false);
            _agent.isStopped = true;
        }

        public override void Update()
        {
            _distance = Vector3.Distance(_body.position, _player.transform.position);
            if (_distance > _distanceDetect)
            {
                _stateSwitcher.SwitchState<Waiting>();
            }
            if (_distance < _config.DistanceAttack || PlayerAboveMe(_body))
            {
                _stateSwitcher.SwitchState<Attack>();
            }
            _agent.SetDestination(_player.transform.position);
        }
    }
}