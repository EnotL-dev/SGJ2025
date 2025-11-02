using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Shape/Wawe")]
    public class Wawe : Shape
    {
        public override string nameNode { get => "Волна"; }
        public override int cost { get => 2; }

        public override int weight { get => 2; }
    }
}
