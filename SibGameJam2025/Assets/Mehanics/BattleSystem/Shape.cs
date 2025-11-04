using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Shape")]
    public class Shape : Node
    {
        private SpellBullet _prefabSpellBullet;

        public virtual SpellBullet prefabSpellBullet
        {
            get => _prefabSpellBullet;
            set => _prefabSpellBullet = value;
        }

        private GameObject _prefabDestroyEffect;

        public virtual GameObject prefabDestroyEffect
        {
            get => _prefabDestroyEffect;
            set => _prefabDestroyEffect = value;
        }

        private AudioClip _spawnSound;

        public virtual AudioClip spawnSound
        {
            get => _spawnSound;
            set => _spawnSound = value;
        }

        private int _damage = 5;

        public virtual int damage
        {
            get => _damage;
            set => _damage = value;
        }

        private int _speed = 25;

        public virtual int speed
        {
            get => _speed;
            set => _speed = value;
        }

        private int _distance = 15;

        public virtual int distance
        {
            get => _distance;
            set => _distance = value;
        }
    }
}