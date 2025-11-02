using UnityEngine;

namespace EnemySystem.Ghost
{
    [CreateAssetMenu(fileName = "GhostConfig", menuName = "Configs/Enemy/Create GhostConfig")]
    public class GhostConfig : ScriptableObject
    {
        public float DistanceAttack => _distanceAttack;
        public int Damage => _damage;
        public float AttackPrepareTime => _attackPrepareTime;
        public float AttackTime => _attackTime;
        public float AttackFrequency => _attackFrequency;
        public float Speed => _speed;

        [SerializeField] private float _distanceAttack = 6f;
        [SerializeField] private int _damage = 5;
        [SerializeField] private float _attackPrepareTime = 0.5f;
        [SerializeField] private float _attackTime = 0.5f;
        [SerializeField] private float _attackFrequency = 0.5f;
        [SerializeField] private float _speed = 10f;
    }
}