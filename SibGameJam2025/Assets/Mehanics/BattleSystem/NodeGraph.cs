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

        public NodeGraph()
        {
            NodeData nodeData = Resources.Load<NodeData>("Nodes/NodeData");

            this.summon = nodeData.summons[0];
            this.shape = nodeData.shapes[0];
            this.impact = new NothingImpact();
            this.feature = new NothingFeature();

            nodeData = null;
            Resources.UnloadAsset(nodeData);
        }

        public int GetManaCostWithChange(Node nodeChange) //Вернет цену с замененным элементом
        {
            int sum = nodeChange.manaCost;

            if (nodeChange is Summon)
                sum += summon.manaCost;
            else if (nodeChange is Shape)
                sum += shape.manaCost;
            else if (nodeChange is Impact)
                sum += impact.manaCost;
            else if (nodeChange is Feature)
                sum += feature.manaCost;

            return sum;
        }

        public int GetManaCost()
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
