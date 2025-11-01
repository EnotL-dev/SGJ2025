using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Impact/Nothing")]
    public class NothingImpact : Impact
    {
        public override string nameNode { get => "Ничего"; }
        public override int cost { get => 0; }
    }
}
