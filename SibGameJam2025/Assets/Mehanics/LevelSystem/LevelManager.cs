using BattleSystem;
using EnemySystem;
using PlayerSystem;
using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelSystem
{
    public class LevelManager : MonoBehaviour
    {
        private WellScript fountainScript;
        [SerializeField] private GameObject soulPrefab;
        [Space(5)]
        [SerializeField] private CanvasGroup hintGroup;
        [SerializeField] private TextMeshProUGUI textHint;
        [Space(5)]
        [SerializeField] private Canvas canvasForNewCpgnitTexts;
        [SerializeField] private TextMeshProUGUI textNewCognition;
        [Space(5)]
        [SerializeField] private PortalTransitZone PORTAL_TRANSIT_ZONE;
        [SerializeField] private TransitionScript transitionScript;

        public void InitializeLevel(int maxSouls) //Инициализируем на старте
        {
            fountainScript = FindFirstObjectByType<WellScript>();
            UpdateUIFountain(0, maxSouls);

            SaveData.currentSouls = 0;
            SaveData.maxSouls = maxSouls;
            PlayerRefs.Instance.PlayerStastController.SoulsUpdate();
        }

        public void Death()
        {
            MusicManager musicManager = FindAnyObjectByType<MusicManager>();
            if (musicManager)
            {
                musicManager.MuffleMusic();
                musicManager.MuffleSound();
            }

            transitionScript.StartTransit(true); //true = для смерти
        }

        public void LoadNextLevel()
        {
            transitionScript.StartTransit(false);
        }

        private void UpdateUIFountain(int min, int max)
        {
            fountainScript.sliderFountainSouls.value = min;
            fountainScript.sliderFountainSouls.maxValue = max;
            fountainScript.textFountainSouls.text = $"{min}/{max}";
        }

        public void SoulAdd(ItemDropper itemDropper)
        {
            SaveData.currentSouls++;
            itemDropper.Drop(fountainScript.transform.position);
            SoulBehaviour soul = Instantiate(soulPrefab, itemDropper.gameObject.transform).GetComponent<SoulBehaviour>();
            soul.transform.parent = null;
            soul.Init(itemDropper.gameObject.transform);

            if (SaveData.currentSouls == SaveData.maxSouls)
                LevelComplete();

            UpdateUIFountain(SaveData.currentSouls, SaveData.maxSouls);
            PlayerRefs.Instance.PlayerStastController.SoulsUpdate();
        }

        public void LevelComplete()
        {
            Debug.Log("Level complete");
            SaveData.TempData.playerParams.LvlUp();
            PlayerRefs.Instance.PlayerStastController.LvUpdate();

            textHint.text = "Этаж зачищен\nУровень прозрения повышен";

            if(PORTAL_TRANSIT_ZONE)
                PORTAL_TRANSIT_ZONE.gameObject.SetActive(true);

            StartCoroutine(ShowsNewNodeNames());
            StartCoroutine(FadeInCoroutine(1));
        }

        public IEnumerator ShowsNewNodeNames()
        {
            NodeData nodeData = Resources.Load<NodeData>("Nodes/NodeData");
            List<Node> newNodes = nodeData.GetNewCognitionNodesList();

            for (int i = 0; i < newNodes.Count; i++)
            {
                TextMeshProUGUI newCognitText = Instantiate(textNewCognition, canvasForNewCpgnitTexts.transform);
                newCognitText.text = $"Вы познали <color=yellow>{newNodes[i].nameNode}</color>";
                yield return new WaitForSeconds(1f);
            }
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