using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    public class NodeGraph
    {
        public Summon summon;
        public Shape shape;
        public Impact impact;
        public Feature feature;

        public NodeGraph(Summon summon, Shape shape, Impact impact, Feature feature)
        {
            this.summon = summon;
            this.shape = shape;
            this.impact = impact;
            this.feature = feature;
        }

        public int cost()
        {
            return summon.manaCost + shape.manaCost + impact.manaCost + feature.manaCost;
        }

        public List<Node> GetNodesInList()
        {
            List<Node> newList = new List<Node>();
            newList.Add(summon);
            newList.Add(shape);
            newList.Add(impact);
            newList.Add(feature);

            return newList;
        }
    }
}
