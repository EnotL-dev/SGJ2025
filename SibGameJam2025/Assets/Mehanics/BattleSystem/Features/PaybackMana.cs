using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "PaybackMana", menuName = "Nodes/Feature/PaybackMana")]
    public class PaybackMana : Feature
    {
        public override string nameNode { get => "Кешбек маны"; }
        public override string description { get => $"Возвращает <b>1/3</b> <color=blue>маны</color> от урона по монстру"; }
        public override int manaCost { get => 5; }
        public override int weight { get => 3; }
        public override int moneyCost { get => 170; }
    }
}
