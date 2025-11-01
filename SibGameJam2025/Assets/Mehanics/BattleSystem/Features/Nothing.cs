using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Nothing", menuName = "Nodes/Feature/NothingFeature")]
    public class NothingFeature : Feature
    {
        public override string nameNode { get => "Ничего"; }
        public override int cost { get => 0; }
    }
}
