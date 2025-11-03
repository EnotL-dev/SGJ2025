using BattleSystem;
using UnityEngine;

namespace EnemySystem
{
    public class ItemDropper : MonoBehaviour
    {
        [SerializeField] private ItemNode _item;
        [SerializeField, Range(0, 100)] private int _chance = 100;
        [SerializeField] private float _force;
        [SerializeField] private Health _health;

        private void OnEnable()
        {
            _health.IsOver += Drop;
        }

        private void OnDisable()
        {
            _health.IsOver -= Drop;
        }

        public void Drop()
        {
            int random = Random.Range(1, 101);
            if (_chance >= random)
            {
                ItemNode instance = Instantiate(_item, transform.position, Quaternion.identity);
                if (instance.TryGetComponent(out Rigidbody rigidbody))
                {
                    Vector3 randomDirection = new Vector3(Random.Range(-1, 1f), 1, Random.Range(-1, 1f));
                    rigidbody.AddForce(randomDirection * _force, ForceMode.Impulse);
                }
            }
        }
    }
}