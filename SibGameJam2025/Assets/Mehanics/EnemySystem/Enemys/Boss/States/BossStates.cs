using PlayerSystem;
using UnityEngine;

namespace EnemySystem.Boss
{
    public class BossStates : StateBehaviour
    {
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private float _distanceDetect;
        [SerializeField] private RotateToPlayer _rotor;
        [SerializeField] private BossConfig _config;
        private const int _attackCount = 1;

        protected override void InitializeStates()
        {
            _states.Add(new Waiting(this, transform, PlayerRefs.Instance.CharacterController, _animator, _distanceDetect, _rotor));
            _states.Add(new AttackLunge(this, transform, PlayerRefs.Instance.CharacterController, _config, _animator, _distanceDetect, _rotor));
            _states.Add(new AttackSelect(this, _animator, _rotor, _attackCount, _config));
            
        }

        protected override void OnAwake()
        {
            
        }
    }
}