using System.Collections.Generic;
using UnityEngine;
using SaveSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
        [SerializeField] private TextMeshProUGUI TextNewCardCost;
        [SerializeField] public Color payColor; //цвет после продажи
        [SerializeField] public Color choiceColor; //цвет не трогать карточку
        [SerializeField] public Color choiceColorHandle; //цвет карточку тронули
        [Space(5)]
        [SerializeField] private TextMeshProUGUI textBalance;
        [SerializeField] private TextMeshProUGUI textMainCost;
        [SerializeField] private TextMeshProUGUI textMainDamage;

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
            foreach (Node node in nodeGraph.GetNodesInList())
            {
                listCards[i].textName.text = node.nameNode;
                listCards[i].textDescription.text = node.description;
                listCards[i].textCost.text = node.manaCost.ToString();
                listCards[i].cost = node.manaCost;
                i++;
            }

            newCard.textName.text = newNode.nameNode;
            newCard.textDescription.text = newNode.description;
            newCard.textCost.text = newNode.manaCost.ToString();
            newCard.cost = newNode.manaCost;
            TextNewCardCost.text = $"{newNode.moneyCost} ¤";

            textMainCost.text = nodeGraph.GetManaCost().ToString();
            textBalance.text = $"{SaveData.TempData.GetMoney()} ¤";

            if (newNode is Summon)
                indexCard = 0;
            else if (newNode is Shape)
                indexCard = 1;
            else if (newNode is Impact)
                indexCard = 2;
            else if (newNode is Feature)
                indexCard = 3;

            newCard.cardObj.GetComponent<Image>().color = choiceColor;
            listCards[indexCard].cardObj.GetComponent<Image>().color = choiceColor;
        }

        private int indexCard = 0;
        public void StartInteraction()
        {
            newCard.cardObj.GetComponent<Image>().color = choiceColorHandle;
            listCards[indexCard].cardObj.GetComponent<Image>().color = choiceColorHandle;
        }

        public void StopInteraction()
        {
            newCard.cardObj.GetComponent<Image>().color = choiceColor;
            listCards[indexCard].cardObj.GetComponent<Image>().color = choiceColor;
        }

        public void MakeCraft() //Сначало проведет действия в UI потом перекинет на "мгновенный" крафт
        {
            if(newNode.moneyCost <= SaveData.TempData.GetMoney())
            {
                newCard.cardObj.SetActive(false);
                listCards[indexCard].textName.text = newNode.nameNode;
                listCards[indexCard].textDescription.text = newNode.description;
                listCards[indexCard].textCost.text = newNode.manaCost.ToString();
                listCards[indexCard].cost = newNode.manaCost;

                listCards[indexCard].cardObj.GetComponent<Image>().color = payColor;

                textMainCost.text = $"{nodeGraph.GetManaCostWithChange(newNode)}";

                StartCoroutine(BalanceEncount());
            }
            else
            {
                print("Нет ДЕНЕГ!");
            }
        }

        public void Dissmis()
        {
            spellCraft.Dissmis();
        }

        private IEnumerator BalanceEncount()
        {
            int count = newNode.moneyCost;
            int balanceInText = SaveData.TempData.GetMoney();
            float timeNext = 3/count;
            while (count > 0)
            {
                count--;
                balanceInText--;
                textBalance.text = $"{balanceInText} ¤";
                yield return new WaitForSeconds(timeNext);
            }

            spellCraft.MakeCraft();
            yield return null;
        }
    }
}
