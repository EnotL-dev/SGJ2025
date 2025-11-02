using UnityEngine;
using SaveSystem;

namespace BattleSystem
{
    public class SpellCraft : MonoBehaviour
    {
        [SerializeField] private GameObject canvasCraft;
        private GameObject tempCanvas;
        private ItemChoiceUI itemChoiceUI;

        private Node newNode;
        private GameObject newItem;

        private GameObject playerObj;
        private void Start()
        {
            playerObj = FindFirstObjectByType<CharacterController>().gameObject;
        }

        public void InitCraft(Node newNode, GameObject newItem)
        {
            if (this.newItem == newItem) //так проверяется не открыто ли уже
                return;

            Time.timeScale = 0.01f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            this.newNode = newNode;
            this.newItem = newItem;
            if (tempCanvas)
                Destroy(tempCanvas);

            tempCanvas = Instantiate(canvasCraft);
            itemChoiceUI = tempCanvas.GetComponent<ItemChoiceUI>();
            itemChoiceUI.InitUI(newNode, this);
        }

        public void MakeCraft()
        {
            SaveData.TempData.money -= newNode.moneyCost;
            Destroy(newItem); //Предмет потрачен
            EndCraft();
        }

        public void Dissmis()
        {
            newItem = null;
            EndCraft();
        }

        private void EndCraft()
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (tempCanvas) Destroy(tempCanvas);
        }
    }
}