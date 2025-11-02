using PlayerSystem;
using UnityEngine;
using UnityEngine.AI;

namespace EnemySystem.Head
{
    public class HeadStates : StateBehaviour
    {
        [SerializeField] private HeadConfig _config;
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _distanceDetect = 10f;
        [SerializeField] private Health _health;
        [SerializeField] private Collider _collision;

        protected override void InitializeStates()
        {
            _states.Add(new Waiting(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _distanceDetect));
            _states.Add(new Death(this, _animator));
            _states.Add(new Attack(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, PlayerRefs.Instance.Health, _agent));
            _states.Add(new Moving(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _agent, _distanceDetect));
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
        }
    }
}