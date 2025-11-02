using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Shape/Beam")]
    public class Beam : Shape
    {
        public override string nameNode { get => "Ëó÷"; }
        public override int manaCost { get => 3; }

        public override int weight { get => 3; }
    }
}
