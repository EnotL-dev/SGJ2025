using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Shape/Sphere")]
    public class Sphere : Shape
    {
        [SerializeField] private SpellBullet newPrefabSpellBullet;
        public override SpellBullet prefabSpellBullet { get => newPrefabSpellBullet; }

        [SerializeField] private AudioClip _spawnSound;
        public override AudioClip spawnSound { get => _spawnSound; }

        public override string nameNode { get => "Шар"; }
        public override string description { get => $"Задает форму заклинанию в виде <b>шара</b>. Назначает урон <color=red>{damage + 1} + Lv*3</color>"; }
        public override int manaCost { get => 1; }
        public override int weight { get => 1; }
        public override int moneyCost { get => 45; }

        public override int damage { get => 5; }
        public override int speed { get => 30; }
        public override int distance { get => 40; }
    }
}
