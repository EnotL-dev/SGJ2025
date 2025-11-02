using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Shape/Sphere")]
    public class Sphere : Shape
    {
        public override string nameNode { get => "Øàð"; }

        public override int weight { get => 4; }

        public override int distance { get => 25; }
    }
}
