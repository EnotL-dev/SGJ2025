using UnityEngine;

namespace EnemySystem.Minotaur
{
    [CreateAssetMenu(fileName = "MinotaurConfig", menuName = "Configs/Enemy/Create MinotaurConfig")]
    public class MinotaurConfig : ScriptableObject
    {
        public float DistanceAttack => _distanceAttack;
        public int Damage => _damage;
        public float AttackPrepareTime => _attackPrepareTime;
        public float AttackTime => _attackTime;
        public float AttackFrequency => _attackFrequency;

        [SerializeField] private float _distanceAttack = 6f;
        [SerializeField] private int _damage = 5;
        [SerializeField] private float _attackPrepareTime = 0.5f;
        [SerializeField] private float _attackTime = 0.5f;
        [SerializeField] private float _attackFrequency = 0.5f;
    }
}