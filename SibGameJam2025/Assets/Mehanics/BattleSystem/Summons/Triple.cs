using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Triple", menuName = "Nodes/Summon/Triple")]
    public class Triple : Summon
    {
        public override string nameNode { get => "Тройной вызов"; }
        public override int manaCost { get => 3; }

        public override int weight { get => 3; }
    }
}
