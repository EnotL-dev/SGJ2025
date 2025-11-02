using UnityEngine;
using UnityEngine.EventSystems;

namespace BattleSystem
{
    public class CardItemTarget : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            CardItemDrag droppedCard = eventData.pointerDrag?.GetComponent<CardItemDrag>();
            if (droppedCard != null)
            {
                Debug.Log($"Card {droppedCard.name} dropped on {name}");
            }
        }
    }
}
