using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Shape/Wawe")]
    public class Wawe : Shape
    {
        [SerializeField] private SpellBullet newPrefabSpellBullet;
        public override SpellBullet prefabSpellBullet { get => newPrefabSpellBullet; }
        [SerializeField] private GameObject _prefabDestroyEffect;
        public override GameObject prefabDestroyEffect { get => _prefabDestroyEffect; }

        [SerializeField] private AudioClip _spawnSound;
        public override AudioClip spawnSound { get => _spawnSound; }

        public override string nameNode { get => "Волна"; }
        public override string description { get => $"Задает форму заклинанию в виде <b>волны</<b>>. Назначает урон <color=red>{damage + 1} + Lv*3</color>"; }
        public override int manaCost { get => 2; }
        public override int weight { get => 1; }
        public override int moneyCost { get => 55; }

        public override int damage { get => 1; }
        public override int speed { get => 20; }
        public override int distance { get => 22; }
    }
}
