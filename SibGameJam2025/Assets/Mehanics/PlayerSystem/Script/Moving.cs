using UnityEngine;

namespace PlayerSystem
{
    public class Moving : MonoBehaviour
    {
        public bool IsGround = true;
        [SerializeField] private PlayerConfig _config;
        [SerializeField] private Camera _camera;
        [SerializeField] private CharacterController _characterController;
        private Vector3 _moving;

        public void Tick()
        {
            Move();
        }

        private void ResetVelocity()
        {
            _moving = Vector3.zero;
        }

        private void Move()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 cameraRight = _camera.transform.right;
            cameraRight.y = 0;
            cameraRight.Normalize();
            Vector3 cameraForward = _camera.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();
            Vector3 _inputMoving =
                cameraRight * x +
                cameraForward * z;
            _inputMoving = Vector3.ClampMagnitude(_inputMoving, 1);
            if (IsGround)
            {
                _moving = _inputMoving * _config.MovingSpeed;
                _characterController.Move(_moving * Time.deltaTime);
            }
            else
            {
                Vector3 jumpMoving = _moving + _inputMoving * _config.MovingSpeedJump;
                jumpMoving = Vector3.ClampMagnitude(jumpMoving, _config.MovingSpeed);
                _characterController.Move(jumpMoving * Time.deltaTime);
            }
        }
    }
}