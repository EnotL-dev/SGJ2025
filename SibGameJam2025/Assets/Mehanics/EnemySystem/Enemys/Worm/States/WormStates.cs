using BulletSystem;
using PlayerSystem;
using UnityEngine;

namespace EnemySystem.Worm
{
    public class WormStates : StateBehaviour
    {
        [SerializeField] private WormConfig _config;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private Health _health;
        [SerializeField] private RotateToPlayer _BodyRotor;
        [SerializeField] private RotateToPlayer _gunRotor;
        [SerializeField] private Transform _launchPoint;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Collider _collision;
        [SerializeField] private AudioSource _shootSound;
        [SerializeField] private AudioSource _DeathSound;
        [SerializeField] private float _destroyTimer;
        [SerializeField] private float _timeBeforeDestroyAnimation;
        [SerializeField] private DestroyAnimation _destroyAnimation;
        [SerializeField] private float _distanceDetect;

        protected override void InitializeStates()
        {
           _states.Add(new Waiting(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _distanceDetect));
            _states.Add(new Death(this, _animator, _destroyTimer, _timeBeforeDestroyAnimation, transform, _destroyAnimation));
            _states.Add(new Attack(this, _bulletPool, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _launchPoint, _layerMask, _shootSound, _distanceDetect));
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
            _BodyRotor.enabled = false;
            _gunRotor.enabled = false;
            _DeathSound.Play();
            Died?.Invoke();
        }
    }
}