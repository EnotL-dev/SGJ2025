using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ChinemaSystem
{
    public class ChinemaManager : MonoBehaviour
    {
        [System.Serializable]
        public class ComicScreen
        {
            public GameObject obj;
            public float delay = 0;
        }

        [SerializeField] private List<ComicScreen> comicScreen;
        [Space(5)]
        [SerializeField] private CanvasGroup canvasDialogs;
        [SerializeField] private TextMeshProUGUI dialogText;
        [SerializeField] private CanvasGroup canvasTransit;

        void Start()
        {
            FadeOutCoroutine(1);
        }

        private IEnumerator FadeInCoroutine(float duration, bool death)
        {
            canvasTransit.gameObject.SetActive(true);

            float elapsed = 0f;
            canvasTransit.alpha = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                canvasTransit.alpha = Mathf.Clamp01(elapsed / duration);
                yield return null;
            }

            canvasTransit.alpha = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        private IEnumerator FadeOutCoroutine(float duration)
        {
            canvasTransit.gameObject.SetActive(true);

            float elapsed = 0f;
            float startAlpha = canvasTransit.alpha;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                canvasTransit.alpha = Mathf.Clamp01(Mathf.Lerp(startAlpha, 0f, elapsed / duration));
                yield return null;
            }

            canvasTransit.alpha = 0f;
            canvasTransit.gameObject.SetActive(false);
        }
    }
}