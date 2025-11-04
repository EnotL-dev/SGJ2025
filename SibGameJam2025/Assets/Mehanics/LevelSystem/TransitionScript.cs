using SaveSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelSystem
{
    public class TransitionScript : MonoBehaviour
    {
        public CanvasGroup canvasGroupTransition;

        public void Awake()
        {
            Debug.Log("Начат переход яркости");
            SaveData.Load();
            StartCoroutine(FadeOutCoroutine(1f));
        }

        public void StartTransit(bool death)
        {
            StartCoroutine(FadeInCoroutine(1f, death));
        }

        private IEnumerator FadeInCoroutine(float duration, bool death)
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
            LoadLevel(death);
        }

        private void LoadLevel(bool death)
        {
            if (!death)
            {
                SaveData.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
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
