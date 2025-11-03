using UnityEngine;
using UnityEngine.UI;

namespace EnemySystem
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Health _health;

        private void OnEnable()
        {
            _health._current.Changed += UpdateValue;
        }

        private void OnDisable()
        {
            _health._current.Changed -= UpdateValue;
        }

        private void UpdateValue(int old, int value)
        {
           _image.fillAmount = (float)value / (float)_health._max.Value;
        }
    }
}