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

        protected override void InitializeStates()
        {
           // _states.Add(new Waiting(this, transform, PlayerRefs.Instance.CharacterController, _config));
           // _states.Add(new Death(this));
           // _states.Add(new Attack(this, _bulletPool, transform, PlayerRefs.Instance.CharacterController, _config, _launchPoint));
        }

        protected override void OnAwake()
        {    
        }
    }
}