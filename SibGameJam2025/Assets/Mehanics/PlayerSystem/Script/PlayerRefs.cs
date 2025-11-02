using UnityEngine;

namespace PlayerSystem
{
    public class PlayerRefs : MonoBehaviour
    {
        public static PlayerRefs Instance { get; private set; }
        public CharacterController CharacterController => _characterController;
        public Health Health => _health;
        public LayerMask PlayerLayer => _playerLayer;

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Health _health;
        [SerializeField] private LayerMask _playerLayer;

        public void Awake()
        {
            Instance = this;
        }
    }
}