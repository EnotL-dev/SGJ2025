using EnemySystem.Head;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Minotaur
{
    public class Moving : State
    {
        private Transform _body;
        private CharacterController _player;
        private float _distance;
        private MinotaurConfig _config;
        private AnimatorController _animator;
        private NavMeshAgent _agent;
        private float _distanceDetect;
        private AudioSource _walkingSound;

        public Moving(IStateSwitcher stateSwitcher,
            Transform body,
            CharacterController player,
            MinotaurConfig config,
            AnimatorController animator,
            NavMeshAgent agent,
            float distanceDetect,
            AudioSource walkingSound
            ) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _config = config;
            _animator = animator;
            _agent = agent;
            _distanceDetect = distanceDetect;
            _walkingSound = walkingSound;
        }

        public override void Start()
        {
            _animator.SetWalk(true);
            try
            {
                _agent.isStopped = false;//!!!!!!!!!!!!!!! проверить что спан поинты касаются навигации!
            }
            catch (Exception ex)
            {
                Debug.Log($"Nav mesh error {ex.Message}");
            }
            _walkingSound.Play();
        }
        public override void Stop()
        {
            _animator.SetWalk(false);
            try
            {
                _agent.isStopped = true;
            }
            catch (Exception ex)
            {
                Debug.Log($"Nav mesh error {ex.Message}");
            }
            _walkingSound.Stop();
        }

        public override void Update()
        {
            _distance = Vector3.Distance(_body.position, _player.transform.position);
            //if (_distance > _distanceDetect)
            //{
            //    _stateSwitcher.SwitchState<Waiting>();
            //}
            if (_distance < _config.DistanceAttack || PlayerAboveMe(_body))
            {
                _stateSwitcher.SwitchState<Attack>();
            }
            _agent.SetDestination(_player.transform.position);
        }
    }
}