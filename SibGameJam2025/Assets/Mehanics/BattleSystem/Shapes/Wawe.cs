using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Shape/Wawe")]
    public class Wawe : Shape
    {
        public override string nameNode { get => "Волна"; }
        public override int manaCost { get => 2; }

        public override int weight { get => 2; }

        public override int damage { get => 4; }
        public override int speed { get => 8; }
    }
}
