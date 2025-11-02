using PlayerSystem;
using UnityEngine;

namespace EnemySystem
{
    public class RotateToPlayer : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 10f;
        [SerializeField] private bool _yDirection = true;
        [SerializeField] private bool _speed = true;
        [SerializeField] private float _yOffset;
        private CharacterController _player;
        private Vector3 _lookDirection;

        private void Start()
        {
            _player = PlayerRefs.Instance.CharacterController;
        }

        public void Update()
        {
            Vector3 playerPosition = _player.transform.position;
            playerPosition.y += _yOffset;
            _lookDirection = playerPosition - transform.position;
            if (!_yDirection)
                _lookDirection.y = 0;
            
            Quaternion targetRotation = Quaternion.LookRotation(_lookDirection);
            if (_speed)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
            else 
            {
                transform.LookAt(playerPosition);
            }
        }
    }
}