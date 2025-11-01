using UnityEngine;

namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Create PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        public float MovingSpeed => _movingSpeed;
        public float MovingSpeedJump => _movingSpeedJump;
        [SerializeField] private float _movingSpeed = 10f;
        [SerializeField] private float _movingSpeedJump = 10f;
    }
}