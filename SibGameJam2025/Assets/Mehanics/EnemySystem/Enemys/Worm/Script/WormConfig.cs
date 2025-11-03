using UnityEngine;

namespace EnemySystem.Worm
{
    [CreateAssetMenu(fileName = "WormConfig", menuName = "Configs/Enemy/Create WormConfig")]
    public class WormConfig : ScriptableObject
    {
        public float AttackFrequancy => _attackFrequancy;
        public float AttackTime => _attackTime;
        public float AttackAnimationSpeed => _attackAnimationSpeed;
        public float AttackPrepareTime => _attackPrepareTime;
        [SerializeField] private float _attackFrequancy = 1f;
        [SerializeField] private float _attackTime = 1f;
        [SerializeField] private float _attackAnimationSpeed = 2f;
        [SerializeField] private float _attackPrepareTime = 0.5f;
    }
}