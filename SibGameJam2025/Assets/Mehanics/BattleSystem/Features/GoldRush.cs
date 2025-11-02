using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "GoldRush", menuName = "Nodes/Feature/GoldRush")]
    public class GoldRush : Feature
    {
        public override string nameNode { get => "Золотая лихорадка"; }
        public override int manaCost { get => 3; }

        public override int weight { get => 2; }
    }
}
