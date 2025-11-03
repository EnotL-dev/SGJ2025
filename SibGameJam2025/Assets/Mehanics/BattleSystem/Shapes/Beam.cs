using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Shape/Beam")]
    public class Beam : Shape
    {
        [SerializeField] private SpellBullet newPrefabSpellBullet;
        public override SpellBullet prefabSpellBullet { get => newPrefabSpellBullet; }
        public override string nameNode { get => "Луч"; }
        public override string description { get => $"Задает форму заклинанию в виде <b>луча</b>. Назначает урон <color=red>{damage} + Lv</color>"; }
        public override int manaCost { get => 3; }
        public override int weight { get => 3; }
        public override int moneyCost { get => 90; }

        public override int damage { get => 3; }
        public override int speed { get => 35; }
    }
}
