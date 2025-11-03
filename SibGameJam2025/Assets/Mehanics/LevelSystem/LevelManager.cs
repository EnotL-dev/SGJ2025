using PlayerSystem;
using SaveSystem;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        private WellScript fountainScript;
        [SerializeField] private CanvasGroup hintGroup;
        [SerializeField] private TextMeshProUGUI textHint;

        PlayerRefs playerRefs = new PlayerRefs();
        public void InitializeLevel(int maxSouls) //Инициализируем на старте
        {
            fountainScript = FindFirstObjectByType<WellScript>();
            UpdateUIFountain(0, maxSouls);

            SaveData.Save(); //На новом уровне - сохранение

            SaveData.currentSouls = 0;
            SaveData.maxSouls = maxSouls;
            playerRefs.PlayerStastController.SoulsUpdate();
        }

        private void UpdateUIFountain(int min, int max)
        {
            fountainScript.sliderFountainSouls.value = 0;
            fountainScript.sliderFountainSouls.maxValue = max;
            fountainScript.textFountainSouls.text = $"{min}/{max}";
        }

        public void SoulAdd()
        {
            SaveData.currentSouls++;

            if(SaveData.currentSouls == SaveData.maxSouls)
                LevelComplete();

            UpdateUIFountain(SaveData.currentSouls, SaveData.maxSouls);
            playerRefs.PlayerStastController.SoulsUpdate();
        }

        public void LevelComplete()
        {
            Debug.Log("Level complete");
            SaveData.TempData.playerParams.LvlUp();

            textHint.text = "Этаж зачищен\nУровень прозрения повышен";
            StartCoroutine(FadeInCoroutine(1));
        }

        public void NewWaweMessage()
        {
            textHint.text = "Грядет новая волна";
            StartCoroutine(FadeInCoroutine(1));
        }

        private IEnumerator FadeInCoroutine(float duration)
        {
            hintGroup.gameObject.SetActive(true);

            float elapsed = 0f;
            hintGroup.alpha = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                hintGroup.alpha = Mathf.Clamp01(elapsed / duration);
                yield return null;
            }

            hintGroup.alpha = 1f;

            yield return new WaitForSeconds(1); //подождать
            StartCoroutine(FadeOutCoroutine(duration));
        }

        private IEnumerator FadeOutCoroutine(float duration)
        {
            float elapsed = 0f;
            float startAlpha = hintGroup.alpha;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                hintGroup.alpha = Mathf.Clamp01(Mathf.Lerp(startAlpha, 0f, elapsed / duration));
                yield return null;
            }

            hintGroup.alpha = 0f;
            hintGroup.gameObject.SetActive(false);
        }
    }
}