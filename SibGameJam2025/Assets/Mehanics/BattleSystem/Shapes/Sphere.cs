using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Node", menuName = "Nodes/Shape/Sphere")]
    public class Sphere : Shape
    {
        [SerializeField] private SpellBullet newPrefabSpellBullet;
        public override SpellBullet prefabSpellBullet { get => newPrefabSpellBullet; }
        public override string nameNode { get => "Øàð"; }

        public override int weight { get => 4; }

        public override int distance { get => 25; }
    }
}
