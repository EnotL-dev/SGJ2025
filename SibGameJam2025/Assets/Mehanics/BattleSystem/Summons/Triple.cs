using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Triple", menuName = "Nodes/Summon/Triple")]
    public class Triple : Summon
    {
        public override string nameNode { get => "Тройной вызов"; }
        public override string description { get => $"Вызывает <b>три</b> копии заклинания за раз"; }
        public override int manaCost { get => 5; }
        public override int weight { get => 2; }
        public override int moneyCost { get => 70; }
    }
}
