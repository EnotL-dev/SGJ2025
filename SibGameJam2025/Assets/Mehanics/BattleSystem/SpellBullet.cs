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

        private Vector3 _startPosition;
        private void Launch(int damage, float speed, float maxDistance)
        {
            this.damage = damage;
            this.speed = speed;
            this.maxDistance = maxDistance;

            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
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

        private void OverDistanceAction()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsLayerInMask(other.gameObject.layer, _layerMaskIgnore))
                return;
            if (IsLayerInMask(other.gameObject.layer, _layerMaskHit))
            {
                if (other.gameObject.TryGetComponent(out Health health))
                {
                    health.Reduce(damage);
                }
            }

            Destroy(gameObject);
        }

        public static bool IsLayerInMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}
