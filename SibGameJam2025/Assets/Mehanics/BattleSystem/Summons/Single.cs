using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Single", menuName = "Nodes/Summon/Single")]
    public class Single : Summon
    {
        public override string nameNode { get => "Одиночный вызов"; }
        public override string description { get => $"Вызывает <b>одну</b> копию заклинание за раз"; }
        public override int manaCost { get => 1; }
        public override int weight { get => 1; }
        public override int moneyCost { get => 35; }
    }
}
