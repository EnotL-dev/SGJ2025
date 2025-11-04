using UnityEngine;

namespace PlayerSystem
{
    public class CameraRotation : MonoBehaviour
    {
        public float mouseSensitivity = 100f;
        public float gamepadSensitivity = 2f;
        public bool invertY = false;
        public float minVerticalAngle = -90f;
        public float maxVerticalAngle = 90f;
        private float xRotation = 0f;
        private float yRotation = 0f;
        private Transform playerBody;

        void Start()
        {
            playerBody = transform.parent;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if (Time.timeScale < 1)
                return;

            HandleMouseLook();
        }

        void HandleMouseLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            ApplyRotation(mouseX, mouseY);
        }


        void ApplyRotation(float horizontal, float vertical)
        {
            if (invertY)
                vertical = -vertical;

            xRotation -= vertical;
            xRotation = Mathf.Clamp(xRotation, minVerticalAngle, maxVerticalAngle);
            yRotation += horizontal;

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}