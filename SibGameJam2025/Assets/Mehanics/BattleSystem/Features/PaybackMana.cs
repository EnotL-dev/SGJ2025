using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "PaybackMana", menuName = "Nodes/Feature/PaybackMana")]
    public class PaybackMana : Feature
    {
        public override string nameNode { get => "Кешбек маны"; }
        public override int cost { get => 4; }

        public override int weight { get => 3; }
    }
}
