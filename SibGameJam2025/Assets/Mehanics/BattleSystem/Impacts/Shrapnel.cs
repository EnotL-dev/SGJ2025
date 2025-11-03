using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Impact/Shrapnel")]
    public class Shrapnel : Impact
    {
        public override string nameNode { get => "Шрапнель"; }
        public override string description { get => $"<b>Расщепляет</b> заклинание на <b>4</b> новые копии при касании объектов или монстров"; }
        public override int manaCost { get => 5; }
        public override int weight { get => 3; }
        public override int moneyCost { get => 165; }
    }
}
