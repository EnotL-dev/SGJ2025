using System;
using UnityEngine;

namespace EnemySystem.Boss
{
    [CreateAssetMenu(fileName = "BossConfig", menuName = "Configs/Enemy/Create BossConfig")]
    public class BossConfig : ScriptableObject
    {
        public AttackLungeConst AttackLunge => _attackLunge;
        public float TimeBeforeAttack => _timeBeforeAttack;
        [SerializeField] private float _timeBeforeAttack = 1f;
        [SerializeField] private AttackLungeConst _attackLunge = new();

        [Serializable]
        public class AttackLungeConst
        {
            public float Time => _time;

            [SerializeField] private float _time = 30f;
            public float DistanceAfterPlayer => _distanceAfterPlayer;

            [SerializeField] private float _distanceAfterPlayer = 5f;
            public AnimationCurve Acceleration => _acceleration;

            [SerializeField] private AnimationCurve _acceleration;
            public float DistanceToDamage => _distanceToDamage;

            [SerializeField] private float _distanceToDamage;

            public int Damage => _damage;

            [SerializeField] private int _damage;
        }
    }
}