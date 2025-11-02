using UnityEngine;

namespace PlayerSystem
{
    public class PlayerRefs : MonoBehaviour
    {
        public static PlayerRefs Instance { get; private set; }
        public CharacterController CharacterController => _characterController;
        public Health Health => _health;

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Health _health;

        public void Awake()
        {
            Instance = this;
        }
    }
}