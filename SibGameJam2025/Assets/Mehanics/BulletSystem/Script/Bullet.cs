using UnityEngine;

namespace BulletSystem
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _maxDistance = 20f;
        private Vector3 _startPosition;
        private BulletPool _pool;

        public void Launch()
        {
            gameObject.SetActive(true);
            gameObject.transform.parent = null;
            _startPosition = transform.position;
        }

        private void SetParentLink(BulletPool pool)
        {
            _pool = pool;
        }

        private void Update()
        {
            Move();
            CheckDistance();
        }

        private void Move()
        {
            transform.Translate(transform.forward * _speed * Time.deltaTime);
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(_startPosition, transform.position) < _maxDistance)
            {
                OverDistanceAction();
            }
        }

        private void OverDistanceAction()
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(_pool.transform);
        }
    }
}