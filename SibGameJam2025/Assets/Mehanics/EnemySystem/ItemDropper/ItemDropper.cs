using BattleSystem;
using UnityEngine;

namespace EnemySystem
{
    public class ItemDropper : MonoBehaviour
    {
        [SerializeField] private ItemNode _item;
        [SerializeField, Range(0, 100)] private int _chance = 100;

        private float spawnForce = 1.9f;    
        private float radius = 0.2f;        
        private float upwardForce = 7.5f;   
        private float maxHorizontalImpulse = 2f; 

        public void Drop(Vector3 pos)
        {
            pos.y += 1.8f;
            int rnd = Random.Range(0, 99);
            if (rnd > _chance)
                return;

            float angle = Random.Range(0f, Mathf.PI * 2f);
            Vector3 spawnPos = pos + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;

            ItemNode item = Instantiate(_item, spawnPos, Quaternion.identity);

            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb == null) return;

            Vector3 horizontalDir = (spawnPos - pos);
            horizontalDir.y = 0f;
            horizontalDir = horizontalDir.normalized;

            float horizontalStrength = spawnForce * Random.Range(0.6f, 1f);

            Vector3 horizontalImpulse = horizontalDir * horizontalStrength;

            if (horizontalImpulse.magnitude > maxHorizontalImpulse)
                horizontalImpulse = horizontalImpulse.normalized * maxHorizontalImpulse;

            Vector3 verticalImpulse = Vector3.up * upwardForce;

            rb.AddForce(horizontalImpulse + verticalImpulse, ForceMode.Impulse);
        }
    }
}