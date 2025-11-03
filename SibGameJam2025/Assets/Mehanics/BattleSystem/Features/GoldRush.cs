using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "GoldRush", menuName = "Nodes/Feature/GoldRush")]
    public class GoldRush : Feature
    {
        public override string nameNode { get => "Золотая лихорадка"; }
        public override string description { get => $"Увеличивает количество выпадаемых <color=yellow>монет</color> на <b>3</b>"; }
        public override int manaCost { get => 3; }
        public override int weight { get => 2; }
        public override int moneyCost { get => 110; }
    }
}
