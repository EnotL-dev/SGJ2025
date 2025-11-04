using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Machinegun", menuName = "Nodes/Impact/Machinegun")]
    public class Machinegun : Impact
    {
        public override string nameNode { get => "Пулемет"; }
        public override string description { get => $"Сокращает скорость перезарядки заклинания в <color=green>2</color> раза"; }
        public override int manaCost { get => 2; }
        public override int weight { get => 2; }
        public override int moneyCost { get => 190; }
    }
}
