using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Vampirism", menuName = "Nodes/Feature/Vampirism")]
    public class Vampirism : Feature
    {
        public override string nameNode { get => "Вампиризм"; }
        public override int manaCost { get => 5; }

        public override int weight { get => 3; }
    }
}
