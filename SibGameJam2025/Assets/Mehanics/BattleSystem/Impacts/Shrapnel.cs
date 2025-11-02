using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Impact/Shrapnel")]
    public class Shrapnel : Impact
    {
        public override string nameNode { get => "Шрапнель"; }
        public override int manaCost { get => 3; }

        public override int weight { get => 2; }
    }
}
