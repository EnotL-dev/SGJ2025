using BattleSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace EnemySystem
{
    public class Cross : MonoBehaviour
    {
        [SerializeField] private Color _hitColor;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Image _cross;
        [SerializeField] private Image _crossFrame;
        [SerializeField] private float _hitTime = 0.1f;
        [SerializeField] private SpellSummon _spellSummon;
        private Coroutine _coroutine;

        private void OnEnable()
        {
            _spellSummon.OnHit += ShowHit;
        }

        private void OnDisable()
        {
            _spellSummon.OnHit -= ShowHit;
        }

        public void ShowHit()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            ShowHitColor();
            yield return new WaitForSeconds(_hitTime);
            HideHitColor();
        }

        private void Start()
        {
            HideHitColor();
        }

        private void ShowHitColor()
        {
            _crossFrame.gameObject.SetActive(true);
            //_cross.color = _hitColor;
            _crossFrame.color = _hitColor;
        }

        private void HideHitColor()
        {
            _crossFrame.gameObject.SetActive(false);
         //   _cross.color = _normalColor;
            _crossFrame.color = _normalColor;
        }
    }
}