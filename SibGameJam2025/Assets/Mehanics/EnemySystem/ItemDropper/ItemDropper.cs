using BattleSystem;
using UnityEngine;

namespace EnemySystem
{
    public class ItemDropper : MonoBehaviour
    {
        [SerializeField] private ItemNode _item;
        [SerializeField, Range(0, 100)] private int _chance = 100;

        private float spawnForce = 5f;
        private float radius = 0.3f;
        private float upwardForce = 4.1f;

        public void Drop(Vector3 pos)
        {
            int rnd = Random.Range(0, 99);
            if (rnd > _chance)
                return;

            float angle = Random.Range(0f, Mathf.PI * 2f);

            Vector3 spawnPos = pos + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;

            ItemNode item = Instantiate(_item, pos, Quaternion.identity);

            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb == null) return;

            Vector3 dir = (spawnPos - pos).normalized + Vector3.up * 0.5f;

            rb.AddForce(dir * spawnForce + Vector3.up * upwardForce, ForceMode.Impulse);
        }
    }
}