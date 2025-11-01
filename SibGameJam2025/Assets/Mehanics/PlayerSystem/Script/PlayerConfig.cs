using UnityEngine;

namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Create PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public float MovingSpeed => _movingSpeed;
        public float MovingSpeedJump => _movingSpeedJump;
        public float JumpHeight => _jumpHeight;
        public float Gravity => _gravity;
        public float GroundCheckDistance => _groundCheckDistance;
        public LayerMask GroundCheckMask => _groundCheckMask;
        [SerializeField] private float _movingSpeed = 10f;
        [SerializeField] private float _movingSpeedJump = 10f;
        [SerializeField] private float _jumpHeight = 5f;
        [SerializeField] private float _gravity = 1f;
        [SerializeField] private float _groundCheckDistance = 1f;
        [SerializeField] private LayerMask _groundCheckMask;
    }
}