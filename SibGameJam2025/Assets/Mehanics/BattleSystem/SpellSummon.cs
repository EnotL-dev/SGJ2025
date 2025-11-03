using SaveSystem;
using System.Collections.Generic;
using UnityEngine;


namespace BattleSystem
{
    public class SpellSummon : MonoBehaviour
    {
        [SerializeField] private Transform spawnSpellPoint;
        private NodeGraph nodeGraph => SaveData.TempData.nodeGraph;
        private float delaySpell = 0.5f;
        private float timerDelay = 0;

        private void Update()
        {
            if (timerDelay < 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (Time.timeScale < 1)
                        return;

                    if (gameObject.GetComponent<Mana>()._current.Value >= nodeGraph.GetManaCost())
                    {
                        gameObject.GetComponent<Mana>().Reduce(nodeGraph.GetManaCost());

                        SummonSpell();
                        timerDelay = delaySpell;
                    }
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

            bool triple = false;
            SpellBullet prefabSpellBullet = null;
            foreach(Node node in nodes)
            {
                if(node is Summon nodeSummon)
                {
                    if (nodeSummon is Triple)
                        triple = true;
                }
                else if(node is Shape nodeShape)
                {
                    prefabSpellBullet = nodeShape.prefabSpellBullet;
                }
            }

            SpellBullet spellBullet = Instantiate(prefabSpellBullet, spawnSpellPoint.position, Camera.main.transform.rotation).GetComponent<SpellBullet>();
            spellBullet.Launch(this, nodes);

            if (triple)
            {
                Quaternion rotation = Camera.main.transform.rotation;
                rotation.y -= 30;
                spellBullet = Instantiate(prefabSpellBullet, spawnSpellPoint.position, rotation).GetComponent<SpellBullet>();
                spellBullet.Launch(this, nodes);
                rotation.y += 60;
                spellBullet = Instantiate(prefabSpellBullet, spawnSpellPoint.position, rotation).GetComponent<SpellBullet>();
                spellBullet.Launch(this, nodes);
            }
        }

        public void HandlingHit(GameObject bulletObj, List<Node> nodes, int trueDamage) //Обработка попадания
        {
            foreach (Node node in nodes)
            {
                if (node is Impact nodeImpact)
                {
                    if(nodeImpact.prefabToSpawn)
                    {
                        if(nodeImpact is Shrapnel)
                        {
                            for (int i = 1; i < 5; i++)
                            {
                                float angle = i * 90f;
                                Quaternion rotation = Camera.main.transform.rotation;
                                rotation.y += angle;

                                List<Node> nodesWithoutShrapnel = new List<Node>();

                                SpellBullet newPrefabBullet = null;
                                foreach (Node addnode in nodes)
                                {
                                    if(addnode is not Impact)
                                    {
                                        if (addnode is Shape addShapeNode)
                                            newPrefabBullet = addShapeNode.prefabSpellBullet;
                                        else
                                        {
                                            nodesWithoutShrapnel.Add(addnode);
                                        }
                                    }
                                    else
                                    {
                                        NodeData nodeData = Resources.Load<NodeData>("Nodes/NodeData");
                                        nodesWithoutShrapnel.Add(nodeData.impacts[0]); //nothing ставим
                                        nodeData = null;
                                        Resources.UnloadAsset(nodeData);
                                    }
                                }

                                SpellBullet spellBullet = Instantiate(nodeImpact.prefabToSpawn, bulletObj.transform.position, rotation).GetComponent<SpellBullet>();
                                spellBullet.Launch(this, nodesWithoutShrapnel);
                            }
                        }
                        else
                        {
                            Instantiate(nodeImpact.prefabToSpawn, bulletObj.transform.position, Quaternion.identity);
                        }
                    }
                }
                else if (node is Feature nodeFeature)
                {
                    if (nodeFeature is Vampirism)
                        gameObject.GetComponent<Health>().Add(trueDamage / 2);
                    else if(node is PaybackMana)
                        gameObject.GetComponent<Mana>().Add(trueDamage / 3);
                }
            }

            Destroy(bulletObj);
        }
    }
}