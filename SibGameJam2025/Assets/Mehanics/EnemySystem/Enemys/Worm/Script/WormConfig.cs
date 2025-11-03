using UnityEngine;

namespace EnemySystem.Worm
{
    [CreateAssetMenu(fileName = "WormConfig", menuName = "Configs/Enemy/Create WormConfig")]
    public class WormConfig : ScriptableObject
    {
        public float DistanceAttack => _distanceAttack;
        public float AttackFrequancy => _attackFrequancy;
        public float AttackTime => _attackTime;
        public float AttackAnimationSpeed => _attackAnimationSpeed;
        public float AttackPrepareTime => _attackPrepareTime;
        [SerializeField] private float _distanceDetect = 5f;
        [SerializeField] private float _distanceAttack = 6f;
        [SerializeField] private float _attackFrequancy = 1f;
        [SerializeField] private float _attackTime = 1f;
        [SerializeField] private float _attackAnimationSpeed = 2f;
        [SerializeField] private float _attackPrepareTime = 0.5f;
    }
}