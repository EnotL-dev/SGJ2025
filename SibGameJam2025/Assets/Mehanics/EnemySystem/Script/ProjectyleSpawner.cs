using EnemySystem.Minotaur;
using UnityEngine;

namespace EnemySystem
{
    public class ProjectyleSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _prefab;
        [SerializeField] private MinotaurStates _states;
        [SerializeField] protected Transform _point;

        private void OnEnable()
        {
            _states.Kick += Spawn;
        }

        private void OnDisable()
        {
            _states.Kick -= Spawn;
        }

        public void Spawn()
        {
            Instantiate(_prefab, _point.position, Quaternion.identity);
        }
    }
}