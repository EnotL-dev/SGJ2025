using UnityEngine;

namespace LevelSystem
{
    public class SoulBehaviour : MonoBehaviour
    {
        private Transform wellTransform;
        private float moveSpeed = 25f;
        private float destroyDistance = 0.3f;

        public void Init(Transform wellTransform)
        {
            this.wellTransform = wellTransform;
        }

        private void Update()
        {
            if (wellTransform == null)
                return;

            transform.position = Vector3.MoveTowards(
                transform.position,
                wellTransform.position,
                moveSpeed * Time.deltaTime
            );

            float distance = Vector3.Distance(transform.position, wellTransform.position);
            if (distance <= destroyDistance)
            {
                Destroy(gameObject);
            }
        }
    }
}
