using System.Collections.Generic;
using UnityEngine;

namespace BulletSystem
{
    public class BulletPool : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _count = 1;
        [SerializeField] private Bullet _prefab;
        private List<Bullet> _bullets = new();
        private int _currentIndex = 0;

        public void Launch(Transform launcher)
        {
            if (_bullets.Count == 0 || _bullets[_currentIndex] == null)
            {
                Debug.LogWarning("No bullets in pool");
                return;
            }
            _bullets[_currentIndex].transform.position = launcher.transform.position;
            _bullets[_currentIndex].transform.rotation = launcher.transform.rotation;
            _bullets[_currentIndex].Launch();
            _currentIndex++;
            if (_currentIndex > _bullets.Count - 1)
            {
                _currentIndex = 0;
            }
        }

        private void Start()
        {
            for (var i = 0; i < _bullets.Count; i++)
            {
                Bullet bullet = Instantiate(_prefab, transform.position, Quaternion.identity);
                bullet.transform.parent = transform;
                bullet.gameObject.SetActive(false);
                _bullets.Add(bullet);
            }
        }
    }
}