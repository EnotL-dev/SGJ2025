using UnityEngine;

namespace BulletSystem
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _maxDistance = 20f;
        private Vector3 _startPosition;
        private Transform _parent;

        public void Launch(Transform target)
        {
            gameObject.SetActive(true);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            gameObject.transform.parent = null;
            _startPosition = transform.position;
        }

        public void SetParentLink(Transform parent)
        {
            _parent = parent;
        }

        private void Update()
        {
            Move();
            CheckDistance();
        }

        private void Move()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        private void CheckDistance()
        {
            if (Vector3.Distance(_startPosition, transform.position) > _maxDistance)
            {
                OverDistanceAction();
            }
        }

        private void OverDistanceAction()
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(_parent);
        }
    }
}