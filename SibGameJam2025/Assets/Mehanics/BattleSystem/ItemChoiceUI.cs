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
        [Space(5)]
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip clipCoinClick;
        [SerializeField] private AudioClip clipOpenPanel;
        [SerializeField] private AudioClip clipMakeSpell;
        [SerializeField] private AudioClip clipDismiss;
        [SerializeField] private AudioClip clipNoMoney;
        [SerializeField] private AudioClip clipClosePanel;

        private NodeGraph nodeGraph => SaveData.TempData.nodeGraph;

        private SpellCraft spellCraft;
        private Node newNode;
        public void InitUI(Node newNode, SpellCraft spellCraft)
        {
            audioSource.PlayOneShot(clipOpenPanel);

            this.newNode = newNode;
            this.spellCraft = spellCraft;
            InitializeCards();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Dissmis();
            }
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

            textMainDamage.text = (nodeGraph.shape.damage + SaveData.TempData.playerParams.bonusDamage).ToString();
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
            List<Node> checkList = SaveData.TempData.nodeGraph.GetNodesInList();
            if(newNode.moneyCost <= SaveData.TempData.GetMoney() && !checkList.Contains(newNode))
            {
                audioSource.PlayOneShot(clipMakeSpell);

                newCard.cardObj.SetActive(false);
                listCards[indexCard].textName.text = newNode.nameNode;
                listCards[indexCard].textDescription.text = newNode.description;
                listCards[indexCard].textCost.text = newNode.manaCost.ToString();
                listCards[indexCard].cost = newNode.manaCost;

                listCards[indexCard].cardObj.GetComponent<Image>().color = payColor;

                textMainCost.text = $"{nodeGraph.GetManaCostWithChange(newNode)}";

                buying = false;
                StartCoroutine(BalanceEncount());
            }
            else
            {
                audioSource.PlayOneShot(clipNoMoney);
                print("Нет ДЕНЕГ!");
            }
        }

        public void Dissmis()
        {
            if (!buying)
            {
                spellCraft.Dissmis();
                audioSource.PlayOneShot(clipDismiss);
            }
        }

        bool buying = false;
        private IEnumerator BalanceEncount()
        {
            buying = true;

            int count = newNode.moneyCost;
            int balanceInText = SaveData.TempData.GetMoney();
            float timeNext = 2/count;
            while (count > 0)
            {
                count--;
                balanceInText--;
                textBalance.text = $"{balanceInText} ¤";

                audioSource.PlayOneShot(clipCoinClick);
                yield return new WaitForSeconds(timeNext);
            }

            audioSource.PlayOneShot(clipClosePanel);
            spellCraft.MakeCraft();
            yield return null;
        }
    }
}
