using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Vampirism", menuName = "Nodes/Feature/Vampirism")]
    public class Vampirism : Feature
    {
        public override string nameNode { get => "Вампиризм"; }
        public override string description { get => $"Возвращает <b>1/2</b> <color=red>здоровья</color> от урона по монстру"; }
        public override int manaCost { get => 5; }
        public override int weight { get => 3; }
        public override int moneyCost { get => 210; }
    }
}
