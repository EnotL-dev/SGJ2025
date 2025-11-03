using UnityEngine;
using SaveSystem;
using System.Collections;
using PlayerSystem;

namespace BattleSystem
{
    public class SpellCraft : MonoBehaviour
    {
        [SerializeField] private PlayerStatsController playerStatsController;
        [SerializeField] private GameObject canvasCraft;
        private GameObject tempCanvas;
        private CanvasGroup canvasGroup;
        private ItemChoiceUI itemChoiceUI;

        private Node newNode;
        private GameObject newItem;

        private GameObject playerObj;
        private CameraRotation cameraRotation;
        private void Start()
        {
            playerObj = FindFirstObjectByType<CharacterController>().gameObject;
            cameraRotation = playerObj.GetComponentInChildren<CameraRotation>();
        }

        public void InitCraft(Node newNode, GameObject newItem)
        {
            if (this.newItem == newItem) //так проверяется не открыто ли уже
                return;

            Time.timeScale = 0.01f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            cameraRotation.enabled = false;
            this.newNode = newNode;
            this.newItem = newItem;
            if (tempCanvas)
                Destroy(tempCanvas);

            tempCanvas = Instantiate(canvasCraft);
            canvasGroup = tempCanvas.GetComponent<CanvasGroup>();
            itemChoiceUI = tempCanvas.GetComponent<ItemChoiceUI>();
            itemChoiceUI.InitUI(newNode, this);

            StartCoroutine(FadeInCoroutine(1f));
        }

        private void AddNodeInNodeGraph()
        {
            if (newNode is Summon)
                SaveData.TempData.nodeGraph.summon = newNode as Summon;
            else if (newNode is Shape)
                SaveData.TempData.nodeGraph.shape = newNode as Shape;
            else if (newNode is Impact)
                SaveData.TempData.nodeGraph.impact = newNode as Impact;
            else if (newNode is Feature)
                SaveData.TempData.nodeGraph.feature = newNode as Feature;
        }

        public void MakeCraft()
        {
            Debug.Log("Плата прошла!");
            SaveData.TempData.ReduceMoney(newNode.moneyCost);
            playerStatsController.BalanceUpdate();
            AddNodeInNodeGraph();
            newNode = null;

            StartCoroutine(FadeOutCoroutine(1f));
            Destroy(newItem); //Предмет потрачен
        }

        public void Dissmis()
        {
            newItem = null;
            StartCoroutine(FadeOutCoroutine(1f));
        }

        private IEnumerator FadeInCoroutine(float duration)
        {
            float elapsed = 0f;
            canvasGroup.alpha = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime * 90;
                canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
                yield return null;
            }

            canvasGroup.alpha = 1f;
        }

        private IEnumerator FadeOutCoroutine(float duration)
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cameraRotation.enabled = true;

            float elapsed = 0f;
            float startAlpha = canvasGroup.alpha;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime*2;
                canvasGroup.alpha = Mathf.Clamp01(Mathf.Lerp(startAlpha, 0f, elapsed / duration));
                yield return null;
            }

            canvasGroup.alpha = 0f;
            if (tempCanvas) Destroy(tempCanvas);
        }
    }
}