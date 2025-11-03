using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Shape/Sphere")]
    public class Sphere : Shape
    {
        [SerializeField] private SpellBullet newPrefabSpellBullet;
        public override SpellBullet prefabSpellBullet { get => newPrefabSpellBullet; }
        public override string nameNode { get => "Шар"; }
        public override string description { get => $"Задает форму заклинанию в виде <b>шара</b>. Назначает урон <color=red>{damage + 1} + Lv*3</color>"; }
        public override int manaCost { get => 1; }
        public override int weight { get => 2; }
        public override int moneyCost { get => 45; }

        public override int distance { get => 25; }
    }
}
