using UnityEngine;

namespace EnemySystem.Worm
{
    [CreateAssetMenu(fileName = "WormConfig", menuName = "Configs/Enemy/Create WormConfig")]
    public class WormConfig : ScriptableObject
    {
        public float DistanceDetect => _distanceDetect;
        public float DistanceAttack => _distanceAttack;
        public float AttackFrequancy => _attackFrequancy;

        [SerializeField] private float _distanceDetect = 5f;
        [SerializeField] private float _distanceAttack = 6f;
        [SerializeField] private float _attackFrequancy = 1f;
    }
}