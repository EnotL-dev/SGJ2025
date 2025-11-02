using System.Collections.Generic;
using UnityEngine;

namespace BulletSystem
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _count = 1;
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _launchPoint;
        private List<Bullet> _bullets = new();
        private int _currentIndex = 0;

        public void Launch(Transform target)
        {
            if (_bullets.Count == 0 || _bullets[_currentIndex] == null)
            {
                Debug.LogWarning("No bullets in pool");
                return;
            }
            _bullets[_currentIndex].Launch(target);
            _currentIndex++;
            if (_currentIndex > _bullets.Count - 1)
            {
                _currentIndex = 0;
            }
        }

        private void Start()
        {
            FillPool();
        }

        private void FillPool()
        {
            for (var i = 0; i < _count; i++)
            {
                Bullet bullet = Instantiate(_prefab, transform.position, Quaternion.identity);
                bullet.transform.parent = _launchPoint;
                bullet.gameObject.SetActive(false);
                bullet.SetParentLink(_launchPoint);
                _bullets.Add(bullet);
            }
        }
    }
}