using UnityEngine;

namespace PlayerSystem
{
    public class PlayerRefs : MonoBehaviour
    {
        public static PlayerRefs Instance { get; private set; }
        public CharacterController CharacterController => _characterController;

        [SerializeField] private CharacterController _characterController;

        public void Awake()
        {
            Instance = this;
        }
    }
}