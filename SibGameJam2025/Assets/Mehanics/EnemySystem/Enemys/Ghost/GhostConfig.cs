using System;
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
        public RecoilConst Recoil => _recoil;

[SerializeField] private float _distanceAttack = 6f;
        [SerializeField] private int _damage = 5;
        [SerializeField] private float _attackPrepareTime = 0.5f;
        [SerializeField] private float _attackTime = 0.5f;
        [SerializeField] private float _attackFrequency = 0.5f;
        [SerializeField] private float _speed = 10f;
        [Space]
        [SerializeField] private RecoilConst _recoil = new();

        [Serializable]
        public class RecoilConst
        {
            [SerializeField] private float _distanceToRecoil = 5f;
            [SerializeField] private float _recoilSpeed = 5f;
            [SerializeField] private float _accuracy = 0.5f;
            [SerializeField] private AnimationCurve _recoilAcceleration;

            public float DistanceToRecoil => _distanceToRecoil;

            public float RecoilSpeed => _recoilSpeed;
            public float Accuracy => _accuracy;

            public AnimationCurve RecoilAcceleration => _recoilAcceleration;
        }
    }
}