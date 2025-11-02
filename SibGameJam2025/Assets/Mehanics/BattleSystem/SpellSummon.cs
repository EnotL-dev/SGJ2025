using SaveSystem;
using System.Collections.Generic;
using UnityEngine;


namespace BattleSystem
{
    public class SpellSummon : MonoBehaviour
    {
        [SerializeField] private Transform spawnSpellPoint;
        [SerializeField] private Transform spawnSpellPointSecond;
        [SerializeField] private Transform spawnSpellPointThird;
        private NodeGraph nodeGraph => SaveData.TempData.nodeGraph;
        private float delaySpell = 0.5f;
        private float timerDelay = 0;

        private void Update()
        {
            if (timerDelay < 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
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

            SpellBullet spellBullet = Instantiate(prefabSpellBullet, spawnSpellPoint.position, Quaternion.identity).GetComponent<SpellBullet>();
            spellBullet.Launch(this, nodes);

            if (triple)
            {
                spellBullet = Instantiate(prefabSpellBullet, spawnSpellPointSecond.position, Quaternion.identity).GetComponent<SpellBullet>();
                spellBullet.Launch(this, nodes);
                spellBullet = Instantiate(prefabSpellBullet, spawnSpellPointThird.position, Quaternion.identity).GetComponent<SpellBullet>();
                spellBullet.Launch(this, nodes);
            }
        }

        public void HandlingHit(GameObject hitObj, GameObject bulletObj, List<Node> nodes, int trueDamage) //Обработка попадания
        {
            foreach (Node node in nodes)
            {
                if (node is Impact nodeImpact)
                {
                    Instantiate(nodeImpact.prefabToSpawn, bulletObj.transform.position, Quaternion.identity);
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