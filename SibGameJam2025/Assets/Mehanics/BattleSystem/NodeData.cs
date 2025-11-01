using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{

    [CreateAssetMenu(fileName = "NodeData", menuName = "NodeData")]
    public class NodeData : ScriptableObject
    {
        public List<Summon> summons;
        public List<Shape> shapes;
        public List<Impact> impacts;
        public List<Feature> features;
    }
}
