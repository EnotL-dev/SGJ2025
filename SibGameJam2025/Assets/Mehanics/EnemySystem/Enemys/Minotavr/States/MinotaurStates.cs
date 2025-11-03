using PlayerSystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Minotaur
{
    public class MinotaurStates : StateBehaviour
    {
        [SerializeField] private MinotaurConfig _config;
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _distanceDetect = 10f;
        [SerializeField] private Health _health;
        [SerializeField] private Collider _collision;
        [SerializeField] private float _destroyTimer;
        [SerializeField] private float _timeBeforeDestroyAnimation;
        [SerializeField] private DestroyAnimation _destroyAnimation;

        protected override void InitializeStates()
        {
            _states.Add(new Waiting(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, (MaxAgro ? 30000 : _distanceDetect)));
            _states.Add(new Death(this, _animator, _destroyTimer, _timeBeforeDestroyAnimation, transform, _destroyAnimation));
            _states.Add(new Attack(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, PlayerRefs.Instance.Health, _agent));
            _states.Add(new Moving(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _agent, 30000));
            SwitchState<Waiting>();
        }

        protected override void OnAwake()
        {

        }

        private void OnEnable()
        {
            _health.IsOver += CallDeath;
        }

        private void OnDisable()
        {
            _health.IsOver -= CallDeath;
        }

        private void CallDeath()
        {
            SwitchState<Death>();
            _collision.enabled = false;
            _agent.enabled = false;
            Died?.Invoke();
        }
    }
}