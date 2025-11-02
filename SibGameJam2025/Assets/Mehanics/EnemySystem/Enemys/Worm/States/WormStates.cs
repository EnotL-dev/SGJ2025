using BulletSystem;
using PlayerSystem;
using UnityEngine;

namespace EnemySystem.Worm
{
    public class WormStates : StateBehaviour
    {
        [SerializeField] private WormConfig _config;
        [SerializeField] private Transform _launchPoint;
        [SerializeField] private BulletPool _bulletPool;
        [SerializeField] private AnimatorController _animator;

        protected override void InitializeStates()
        {
           _states.Add(new Waiting(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator));
            _states.Add(new Death(this));
            _states.Add(new Attack(this, _bulletPool, transform, PlayerRefs.Instance.CharacterController, _config, _launchPoint, _animator));
            SwitchState<Waiting>();
        }

        protected override void OnAwake()
        {    
        }
    }
}