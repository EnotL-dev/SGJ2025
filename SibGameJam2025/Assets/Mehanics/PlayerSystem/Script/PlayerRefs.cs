using LevelSystem;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerRefs : MonoBehaviour
    {
        public static PlayerRefs Instance { get; private set; }
        public CharacterController CharacterController => _characterController;
        public Health Health => _health;
        public LayerMask PlayerLayer => _playerLayer;
        public PlayerStatsController PlayerStastController => _playerStastController;

        public LevelManager levelManager => _levelManager;

        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Health _health;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private PlayerStatsController _playerStastController;
        [SerializeField] private LevelManager _levelManager;

        public void Awake()
        {
            Instance = this;
        }
    }
}