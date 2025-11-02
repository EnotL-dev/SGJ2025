using System.Collections.Generic;
using UnityEngine;
using SaveSystem;
using UnityEngine.UI;
using TMPro;

namespace BattleSystem
{
    public class ItemChoiceUI : MonoBehaviour
    {
        [System.Serializable]
        class CardUI
        {
            [SerializeField] public GameObject cardObj;
            [SerializeField] public TextMeshProUGUI textName;
            [SerializeField] public TextMeshProUGUI textDescription;
            [SerializeField] public TextMeshProUGUI textCost;
            [SerializeField] public int cost;
        }

        [SerializeField] private List<CardUI> listCards;
        [SerializeField] private CardUI newCard = new CardUI();

        private NodeGraph nodeGraph => SaveData.TempData.nodeGraph;

        private SpellCraft spellCraft;
        private Node newNode;
        public void InitUI(Node newNode, SpellCraft spellCraft)
        {
            this.newNode = newNode;
            this.spellCraft = spellCraft;
            InitializeCards();
        }

        private void InitializeCards()
        {
            int i = 0;
            foreach(Node node in nodeGraph.GetNodesInList())
            {
                listCards[i].textName.text = node.nameNode;
                listCards[i].textDescription.text = node.description;
                listCards[i].textCost.text = node.cost.ToString();
                listCards[i].cost = node.cost;
                i++;
            }
        }

        public void MakeCraft()
        {
            spellCraft.MakeCraft();
        }

        public void Dissmis()
        {
            spellCraft.Dissmis();
        }
    }
}
