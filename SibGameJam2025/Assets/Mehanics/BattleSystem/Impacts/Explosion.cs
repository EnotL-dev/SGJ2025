using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Impact/Explosion")]
    public class Explosion : Impact
    {
        public override string nameNode { get => "Взрыв"; }
        public override int cost { get => 4; }
        public override int weight { get => 3; }
    }
}
