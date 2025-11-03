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
        [SerializeField] private Light _pointLight;
        [SerializeField] private AudioSource _deathSound;
        [SerializeField] private AudioSource _attackSound;
        [SerializeField] private float _timeToDestroy;

        protected override void InitializeStates()
        {
            _states.Add(new Waiting(this, transform, PlayerRefs.Instance.CharacterController, _animator, (MaxAgro ? 30000 : _distanceDetect), _rotor));
            _states.Add(new Moving(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, 30000, _rotor));
            _states.Add(new Attack(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, PlayerRefs.Instance.Health, _rotor, _attackSound));
            _states.Add(new Death(this, _animator, _timeToDestroy, transform));
            _states.Add(new Recoil(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _rotor));
            SwitchState<Waiting>();
        }

        protected override void OnAwake()
        {
            
        }

        private void OnEnable()
        {
            _health.IsOver += CallDeath;
            _health._current.Changed += CallRecoil;
            _health._current.Changed += CallAgr;
        }

        private void OnDisable()
        {
            _health.IsOver -= CallDeath;
            _health._current.Changed -= CallRecoil;
            _health._current.Changed -= CallAgr;
        }

        private void CallDeath()
        {
            SwitchState<Death>();
            _collision.enabled = false;
            _rotor.enabled = false;
            _pointLight.enabled = false;
            _deathSound.Play();
            Died?.Invoke();
        }

        private void CallRecoil(int old, int newValue)
        {
            SwitchState<Recoil>();
        }

        private void CallAgr(int old, int newValue)
        {
            if (CurrentState is Waiting)
                SwitchState<Moving>();
        }
    }
}