using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerSystem
{
    public class Jumping : MonoBehaviour
    {
        public bool IsGround { get; set; } = true;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Transform _groundCheckerPosition;
        [SerializeField] private InputAction _input;
        private Vector3 _velocity;

        private void Update()
        {
            CheckGround();
            ApplyGravitation();
            MoveVertical();
            StartJump();
        }

        private void StartJump()
        {
            if (IsGround && CanJump() && _input.triggered)
            {
                Debug.Log("jump");
                _velocity.y = Mathf.Sqrt(_playerConfig.JumpHeight * -2f * _playerConfig.Gravity);
                //PlayOneShot(playerConfig.StartJumpSound);
            }

        }

        private void CheckGround()
        {
            IsGround = Physics.CheckSphere(
                _groundCheckerPosition.position,
                _playerConfig.GroundCheckDistance,
                _playerConfig.GroundCheckMask);
            if (IsGround && _velocity.y < 0)
                _velocity.y = -2f;
        }

        private void ApplyGravitation()
        {
            _velocity.y += _playerConfig.Gravity * Time.deltaTime;
        }

        private void MoveVertical()
        {
            _characterController.Move(_velocity * Time.deltaTime);
        }

        private bool CanJump()
        {
            return !CheckCapsule(
                _characterController.transform.position + Vector3.up * _playerConfig.JumpHeight,
                _characterController.height,
                _characterController.radius,
                _playerConfig.GroundCheckMask
                );
        }

        private bool CheckCapsule(Vector3 bottomPoint, float height, float radius, LayerMask layerMask)
        {
            Vector3 startPoint = bottomPoint;
            startPoint.y += radius;
            Vector3 endPosition = bottomPoint;
            endPosition.y += height - radius;
            return Physics.CheckCapsule(startPoint, endPosition, radius, layerMask);
        }

        //private void PlayLandingSound(bool old, bool current)
        //{
        //    if (old != current && current)
        //        JumpSound.PlayOneShot(_playerConfig.LandingSound);
        //}
    }
}