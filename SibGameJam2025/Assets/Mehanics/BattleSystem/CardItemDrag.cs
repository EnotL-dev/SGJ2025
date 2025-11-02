using UnityEngine;
using UnityEngine.EventSystems;

namespace BattleSystem
{
    public class CardItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float returnSpeed = 4500f;
        [SerializeField] private float hoverScaleFactor = 1.1f;
        [SerializeField] private float enlargeFactor = 1.3f;

        private Transform originalParent;
        private Vector3 startPosition;
        private Canvas canvas;
        private CanvasGroup canvasGroup;

        private bool isReturning = false;
        private Vector3 targetPosition;

        private Vector3 originalScale;
        private Vector3 enlargedScale;
        private Vector3 targetScale;

        private void Awake()
        {
            canvas = GetComponentInParent<Canvas>();
            canvasGroup = GetComponent<CanvasGroup>(); 

            originalScale = transform.localScale;
            targetScale = originalScale;
            enlargedScale = originalScale * enlargeFactor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isDragging)
                transform.localScale = originalScale * hoverScaleFactor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isDragging)
                transform.localScale = originalScale;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            isReturning = false;
            isDragging = true;
            originalParent = transform.parent;
            startPosition = transform.position;

            transform.SetAsLastSibling();
            transform.localScale = enlargedScale;

            if (canvasGroup != null)
                canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            isDragging = false;

            transform.localScale = originalScale;

            bool droppedOnTarget = eventData.pointerEnter != null &&
                                   eventData.pointerEnter.GetComponent<CardItemTarget>() != null;

            if (!droppedOnTarget)
            {
                isReturning = true;
                targetPosition = startPosition;
            }

            if (canvasGroup != null)
                canvasGroup.blocksRaycasts = true;
        }

        private bool isDragging = false;
        private void Update()
        {
            if (!isReturning) return;

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * returnSpeed);

            if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
            {
                transform.position = targetPosition;
                transform.SetParent(originalParent, true);
                isReturning = false;
            }
        }
    }
}
