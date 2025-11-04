using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem
{
    public class CamaraShaker : MonoBehaviour
    {
        [SerializeField] private float shakeDuration = 0.3f;
        [SerializeField] private float shakeMagnitude = 0.05f;
        [SerializeField] private Jumping _jumping;
        [SerializeField] private Health _health;
        [SerializeField] private Vector3 _direction = Vector3.forward;

        private Vector3 initialPosition;
        private bool isShaking = false;

        private void OnEnable()
        {
            if (_jumping != null)
                _jumping.GroundedFromHeight += Shake;
            if (_health != null)
                _health._current.Changed += Shake;
        }

        private void OnDisable()
        {
            if (_jumping != null)
                _jumping.GroundedFromHeight -= Shake;
            if (_health != null)
                _health._current.Changed -= Shake;
        }

        void Start()
        {
            initialPosition = transform.localPosition;
        }

        public void Shake()
        {
            Shake(0, 0);
        }

        public void Shake(int old, int newValue)
        {
            if (newValue > old)
                return;
            if (!isShaking)
            {
                StartCoroutine(ShakeCoroutine());
            }
        }

        private IEnumerator ShakeCoroutine()
        {
            isShaking = true;
            float elapsed = 0f;

            while (elapsed < shakeDuration)
            {
                float currentMagnitude = shakeMagnitude * (1 - (elapsed / shakeDuration));
                Vector3 randomOffset = _direction * Random.Range(-1, 1) * currentMagnitude;
                transform.localPosition = initialPosition + randomOffset;
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.localPosition = initialPosition;
            isShaking = false;
        }
    }
}