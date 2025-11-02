using UnityEngine;

namespace BattleSystem
{
    public class SpellCraft : MonoBehaviour
    {
        [System.Serializable]
        public class UICraft
        {
             
        }

        [SerializeField] private GameObject canvasCraft;

        private Node newNode;

        private GameObject tempCanvas;
        public void InitCraft(Node newNode)
        {
            this.newNode = newNode;
            tempCanvas = Instantiate(canvasCraft);
        }

        private void MakeCraft()
        {
            EndCraft();
        }

        private void EndCraft()
        {
            if(tempCanvas) Destroy(tempCanvas);
        }
    }
}