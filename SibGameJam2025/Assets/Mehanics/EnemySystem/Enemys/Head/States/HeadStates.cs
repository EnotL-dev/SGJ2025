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
        [SerializeField] private float _destroyTimer;
        [SerializeField] private float _timeBeforeDestroyAnimation;
        [SerializeField] private DestroyAnimation _destroyAnimation;
        [SerializeField] private AudioSource _attackSound;
        [SerializeField] private AudioSource _deathSound;

        protected override void InitializeStates()
        {
            _states.Add(new Waiting(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, (MaxAgro ? 30000 : _distanceDetect)));
            _states.Add(new Death(this, _animator, _destroyTimer, _timeBeforeDestroyAnimation, transform, _destroyAnimation));
            _states.Add(new Attack(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, PlayerRefs.Instance.Health, _agent, _attackSound));
            _states.Add(new Moving(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _agent, 30000));
            SwitchState<Waiting>();
        }

        protected override void OnAwake()
        {
       
        }

        private void OnEnable()
        {
            _health.IsOver += CallDeath;
            _health._current.Changed += CallAgr;
        }

        private void OnDisable()
        {
            _health.IsOver -= CallDeath;
            _health._current.Changed -= CallAgr;
        }

        private void CallDeath()
        {
            SwitchState<Death>();
            _collision.enabled = false;
            _agent.enabled = false;
            _deathSound.Play();
            Died?.Invoke();
        }

        private void CallAgr(int old, int newValue)
        {
            if (CurrentState is Waiting)
                SwitchState<Moving>();
        }
    }
}