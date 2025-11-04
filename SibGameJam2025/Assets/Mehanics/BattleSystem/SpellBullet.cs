using EnemySystem;
using PlayerSystem;
using SaveSystem;
using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    [System.Serializable]
    public class SpellBullet : MonoBehaviour
    {
        private float speed = 10f;
        private float maxDistance = 20f;
        private int damage;
        [SerializeField] private LayerMask _layerMaskHit;
        [SerializeField] private LayerMask _layerMaskIgnore;

        private List<Node> nodes; //для обращения что делать к spellSummon
        private SpellSummon spellSummon;

        private Vector3 _startPosition;
        public void Launch(SpellSummon spellSummon, List<Node> nodes)
        {
            this.spellSummon = spellSummon;
            this.nodes = nodes;

            foreach(Node node in nodes)
            {
                if(node is Shape nodeShape)
                {
                    damage = nodeShape.damage + SaveData.TempData.playerParams.bonusDamage;
                    speed = nodeShape.speed;
                    maxDistance = nodeShape.distance;
                }
            }

            gameObject.transform.parent = null;
            _startPosition = transform.position;
        }

        private void Update()
        {
            Move();
            CheckDistance();
        }

        private void Move()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(_startPosition, transform.position) > maxDistance)
            {
                OverDistanceAction();
            }
        }

        private bool hasHit = false;
        private void OverDistanceAction()
        {
            if (!hasHit)
            { 
                hasHit = true;
                spellSummon.HandlingHit(gameObject, nodes, 0);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsLayerInMask(other.gameObject.layer, _layerMaskIgnore))
                return;

            if (IsLayerInMask(other.gameObject.layer, _layerMaskHit))
            {
                if (other.gameObject.TryGetComponent(out Health health) && gameObject.layer != LayerMask.NameToLayer("Player"))
                {
                    int trueDamage = damage; //урон
                    if (health._current.Value <= damage) //Это значит что урон убьет противника
                    {
                        trueDamage = health._current.Value;

                        if(other.gameObject.GetComponent<ItemDropper>())
                            PlayerRefs.Instance.levelManager.SoulAdd(other.gameObject.GetComponent<ItemDropper>());
                    }

                    health.Reduce(damage);

                    if (!hasHit)
                    {
                        hasHit = true;
                        spellSummon.HandlingHit(gameObject, nodes, trueDamage);
                    }
                }
            }
            else if (!IsLayerInMask(other.gameObject.layer, _layerMaskIgnore))
            {
                if (!hasHit)
                {
                    hasHit = true;
                    spellSummon.HandlingHit(gameObject, nodes, 0);
                }
            }
        }

        public static bool IsLayerInMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}
