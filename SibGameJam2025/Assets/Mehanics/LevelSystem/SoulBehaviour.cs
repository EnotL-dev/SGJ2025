using UnityEngine;

namespace LevelSystem
{
    public class SoulBehaviour : MonoBehaviour
    {
        private Transform wellTransform;
        private float moveDuration = 1f;
        private float elapsedTime = 0f;

        private Vector3 startPos;
        private bool isMoving = false;

        public void Init(Transform wellTransform)
        {
            this.wellTransform = wellTransform;
            startPos = transform.position;
            elapsedTime = 0f;
            isMoving = true;
        }

        private void Update()
        {
            if (!isMoving || wellTransform == null)
                return;

            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);

            transform.position = Vector3.Lerp(startPos, wellTransform.position, t * t * (3f - 2f * t));

            if (t >= 1f)
            {
                Destroy(gameObject);
                isMoving = false;
            }
        }
    }
}
