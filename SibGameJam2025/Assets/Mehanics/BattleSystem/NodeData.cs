using SaveSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleSystem
{

    [CreateAssetMenu(fileName = "NodeData", menuName = "NodeData")]
    public class NodeData : ScriptableObject
    {
        public List<Summon> summons;
        public List<Shape> shapes;
        public List<Impact> impacts;
        public List<Feature> features;

        public Node GetRandomNodeByWeightScene()
        {
            int currentSceneIndex = SaveData.TempData.playerParams.lv; //SceneManager.GetActiveScene().buildIndex;

            List<Node> playerNowNodes = new List<Node>();
            playerNowNodes.Add(SaveData.TempData.nodeGraph.summon);
            playerNowNodes.Add(SaveData.TempData.nodeGraph.shape);
            playerNowNodes.Add(SaveData.TempData.nodeGraph.impact);
            playerNowNodes.Add(SaveData.TempData.nodeGraph.feature);

            List<Node> allNodes = new List<Node>();

            foreach(Node node in summons)
            {
                if(playerNowNodes[0] != node)
                    allNodes.Add(node);
            }
            foreach (Node node in shapes)
            {
                if (playerNowNodes[1] != node)
                    allNodes.Add(node);
            }
            foreach (Node node in impacts)
            {
                if (playerNowNodes[2] != node)
                    allNodes.Add(node);
            }
            foreach (Node node in features)
            {
                if (playerNowNodes[3] != node)
                    allNodes.Add(node);
            }

            for (int i = allNodes.Count - 1; i >= 0; i--)
            {
                if (allNodes[i].weight >= currentSceneIndex)
                {
                    allNodes.RemoveAt(i);
                }
            }

            if(allNodes.Count > 0)
            {
                int randNodeIndex = Random.Range(0, allNodes.Count);
                return allNodes[randNodeIndex];
            }

            return null;
        }
    }
}
