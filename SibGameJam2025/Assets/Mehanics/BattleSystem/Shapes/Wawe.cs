using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Shape/Wawe")]
    public class Wawe : Shape
    {
        [SerializeField] private SpellBullet newPrefabSpellBullet;
        public override SpellBullet prefabSpellBullet { get => newPrefabSpellBullet; }
        public override string nameNode { get => "Волна"; }
        public override string description { get => $"Задает форму заклинанию в виде <b>волны</<b>>. Назначает урон <color=red>{damage} + Lv</color>"; }
        public override int manaCost { get => 2; }
        public override int weight { get => 2; }
        public override int moneyCost { get => 85; }

        public override int damage { get => 4; }
        public override int speed { get => 20; }
    }
}
