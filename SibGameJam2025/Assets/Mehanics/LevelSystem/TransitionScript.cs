using System.Collections;
using UnityEngine;

namespace LevelSystem
{
    public class TransitionScript : MonoBehaviour
    {
        public CanvasGroup canvasGroupTransition;

        public void Start()
        {
            Debug.Log("Начат переход яркости");
            StartCoroutine(FadeOutCoroutine(1f));
        }

        public void StartTransit()
        {
            StartCoroutine(FadeInCoroutine(1f));
        }

        private IEnumerator FadeInCoroutine(float duration)
        {
            canvasGroupTransition.gameObject.SetActive(true);

            float elapsed = 0f;
            canvasGroupTransition.alpha = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                canvasGroupTransition.alpha = Mathf.Clamp01(elapsed / duration);
                yield return null;
            }

            canvasGroupTransition.alpha = 1f;
        }

        private IEnumerator FadeOutCoroutine(float duration)
        {
            canvasGroupTransition.gameObject.SetActive(true);

            float elapsed = 0f;
            float startAlpha = canvasGroupTransition.alpha;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                canvasGroupTransition.alpha = Mathf.Clamp01(Mathf.Lerp(startAlpha, 0f, elapsed / duration));
                yield return null;
            }

            canvasGroupTransition.alpha = 0f;
            canvasGroupTransition.gameObject.SetActive(false);
        }
    }
}
