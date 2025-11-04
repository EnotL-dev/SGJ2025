using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Impact/Explosion")]
    public class Explosion : Impact
    {
        public override string nameNode { get => "Взрыв"; }
        public override string description { get => $"<b>Взрывает</b> заклинание при касании объектов или монстров (<color=red>может ранить игрока</color>)"; }
        public override int manaCost { get => 3; }
        public override int weight { get => 2; }
        public override int moneyCost { get => 95; }
    }
}
