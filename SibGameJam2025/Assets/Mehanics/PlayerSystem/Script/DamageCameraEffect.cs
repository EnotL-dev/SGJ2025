using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerSystem
{
    public class DamageCameraEffect : MonoBehaviour
    {
        [SerializeField] private Health _health;
        [SerializeField] private Image _image;
        [SerializeField] private float _dureation;
        [SerializeField] private float _opacitDuration;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            _health._current.Changed += DoEffect;
        }

        private void OnDisable()
        {
            _health._current.Changed -= DoEffect;
        }

        private void DoEffect(int old, int newValue)
        {
            if (newValue > old)
                return;
            if (_health._current.Value <= 0)
            {
                Color col = Color.white;
                col.a = 1;
                _image.color = col;
                return;
            }
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(DamageEffect());
        }

        private IEnumerator DamageEffect()
        {
            Color col = Color.white;
            col.a = 1;
            _image.color = col;
            yield return new WaitForSeconds(_dureation);
            float time = _opacitDuration;
            while (time > 0)
            {
                col.a = time / _opacitDuration;
                time -= Time.deltaTime;
                _image.color = col;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}