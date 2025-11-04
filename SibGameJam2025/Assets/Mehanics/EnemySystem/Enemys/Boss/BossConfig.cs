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

        public AttackExplosionOnPlayerConst AttackExplosionOnPlayer => _attackExplosionOnPlayer;
        [SerializeField] private AttackExplosionOnPlayerConst _attackExplosionOnPlayer = new();
        public SummonConst Summon => _summon;
        [SerializeField] private SummonConst _summon = new();

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

        [Serializable]
        public class AttackExplosionOnPlayerConst
        {
            public float TimeToExplosion => _timeToExplosion;

            [SerializeField] private float _timeToExplosion = 3f;
            public Transform Explosion => _explosion;

            [SerializeField] private Transform _explosion;
            public float EndOfAttackAnimationTime => _endOfAttackAnimationTime;

            [SerializeField] private float _endOfAttackAnimationTime = 3f;

        }

        [Serializable]
        public class SummonConst
        {
            public float TimeBeforeSummon => _timeBeforeSummon;

            [SerializeField] private float _timeBeforeSummon = 1f;
            public float TimeBetweeenSummon => _timeBetweeenSummon;

            [SerializeField] private float _timeBetweeenSummon = 0.5f;
        }
    }
}