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

        protected override void InitializeStates()
        {
           _states.Add(new Waiting(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator));
            _states.Add(new Death(this, _animator));
            _states.Add(new Attack(this, _bulletPool, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _launchPoint, _layerMask));
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
        }
    }
}