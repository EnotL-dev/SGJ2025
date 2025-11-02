using UnityEngine;

namespace BattleSystem
{
    public class SpellCraft : MonoBehaviour
    {
        [SerializeField] private GameObject canvasCraft;
        private GameObject tempCanvas;
        private ItemChoiceUI itemChoiceUI;

        private Node newNode;

        private GameObject playerObj;
        private void Start()
        {
            playerObj = FindFirstObjectByType<CharacterController>().gameObject;
        }

        public void InitCraft(Node newNode)
        {
            Time.timeScale = 0.01f;

            this.newNode = newNode;
            tempCanvas = Instantiate(canvasCraft);
            itemChoiceUI = tempCanvas.GetComponent<ItemChoiceUI>();
            itemChoiceUI.InitUI(newNode, this);
        }

        public void MakeCraft()
        {
            EndCraft();
        }

        public void Dissmis()
        {

        }

        private void EndCraft()
        {
            Time.timeScale = 1f;
            if(tempCanvas) Destroy(tempCanvas);
        }
    }
}