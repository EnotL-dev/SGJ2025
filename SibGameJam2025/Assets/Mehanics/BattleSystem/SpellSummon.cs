using SaveSystem;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace BattleSystem
{
    public class SpellSummon : MonoBehaviour
    {
        public Action OnHit { get; set; }
        [SerializeField] private Transform spawnSpellPoint;
        [SerializeField] private AudioSource audioSpawnSource;
        [SerializeField] private Animator _stickAnimator;
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
                        _stickAnimator.SetTrigger("Attack");
                        SummonSpell();
                        SetNewDelay();
                    }
                }
            }
            else
            {
                timerDelay -= Time.deltaTime;
            }
        }

        private void SetNewDelay()
        {
            if (nodeGraph.impact is not Machinegun)
                timerDelay = delaySpell;
            else
                timerDelay = delaySpell/2;
        }

        private void SummonSpell()
        {
            List<Node> nodes = nodeGraph.GetNodesInList();

            if(audioSpawnSource && nodes[1] is Shape shapeNode)
                audioSpawnSource.PlayOneShot(shapeNode.spawnSound);

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
                Quaternion rotation = Quaternion.Euler(Camera.main.transform.eulerAngles + new Vector3(0, -30f, 0));
                spellBullet = Instantiate(prefabSpellBullet, spawnSpellPoint.position, rotation).GetComponent<SpellBullet>();
                spellBullet.Launch(this, nodes);
                rotation = Quaternion.Euler(Camera.main.transform.eulerAngles + new Vector3(0, 30f, 0));
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
                        Instantiate(nodeImpact.prefabToSpawn, bulletObj.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        if (nodeImpact is Shrapnel)
                        {
                            Vector3 eulerA = bulletObj.transform.eulerAngles;
                            Vector3 bulPos = bulletObj.transform.position;
                            for (int i = 1; i < 5; i++)
                            {
                                Quaternion rotation = Quaternion.Euler(eulerA + new Vector3(0, i * 90f, 0));

                                List<Node> nodesWithoutShrapnel = new List<Node>();

                                SpellBullet newPrefabBullet = null;
                                foreach (Node addnode in nodes)
                                {
                                    if (addnode is not Impact)
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

                                
                                SpellBullet spellBullet = Instantiate(newPrefabBullet, bulPos, rotation).GetComponent<SpellBullet>();
                                spellBullet.Launch(this, nodesWithoutShrapnel);
                            }
                        }
                    }
                }
                else if (node is Feature nodeFeature)
                {
                    if (nodeFeature is Vampirism)
                        gameObject.GetComponent<Health>().Add(trueDamage / 2);
                    else if(nodeFeature is PaybackMana)
                        gameObject.GetComponent<Mana>().Add(trueDamage / 3);
                }
            }
            if (trueDamage > 0)
                OnHit?.Invoke();

            if (bulletObj)
            {
                if (nodes[1] is Shape shapeNode)
                {
                    GameObject tempObj = Instantiate(shapeNode.prefabDestroyEffect, bulletObj.transform);
                    tempObj.transform.parent = null;

                    if (shapeNode is not Wawe)
                        Destroy(bulletObj);
                    else
                        bulletObj.GetComponent<DestroyMyselfByTimer>().enabled = true;
                }
            }
        }
    }
}