using SaveSystem;
using System.Collections.Generic;
using UnityEngine;


namespace BattleSystem
{
    public class SpellSummon : MonoBehaviour
    {
        private NodeGraph nodeGraph => SaveData.TempData.nodeGraph;
        private float delaySpell = 0.5f;
        private float timerDelay = 0;
        private void Update()
        {
            if (timerDelay < 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    SummonSpell();
                    timerDelay = delaySpell;
                }
            }
            else
            {
                timerDelay -= Time.deltaTime;
            }
        }

        private void SummonSpell()
        {
            List<Node> nodes = nodeGraph.GetNodesInList();

            foreach(Node node in nodes)
            {
                if(node is Summon nodeSummon)
                {

                }
                else if(node is Shape nodeShape)
                {

                }
                else if (node is Impact nodeImpact)
                {

                }
                else if (node is Feature nodeFeature)
                {

                }
            }
        }
    }
}