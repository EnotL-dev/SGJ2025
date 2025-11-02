using PlayerSystem;
using UnityEngine;

namespace EnemySystem.Ghost
{
    public class GhostStates : StateBehaviour
    {
        [SerializeField] private GhostConfig _config;
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private float _distanceDetect = 10f;
        [SerializeField] private Collider _collision;
        [SerializeField] private RotateToPlayer _rotor;
        [SerializeField] private Health _health;

        protected override void InitializeStates()
        {
            _states.Add(new Waiting(this, transform, PlayerRefs.Instance.CharacterController, _animator, _distanceDetect, _rotor));
            _states.Add(new Moving(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _distanceDetect, _rotor));
            _states.Add(new Attack(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, PlayerRefs.Instance.Health, _rotor));
            _states.Add(new Death(this, _animator));
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
            _rotor.enabled = false;
        }
    }
}