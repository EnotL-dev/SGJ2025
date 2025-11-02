using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem
{
    public class ItemNode : MonoBehaviour
    {
        private Transform playerTransform;
        [SerializeField] private GameObject canvasObj;
        [SerializeField] private TextMeshProUGUI textName;
        [SerializeField] private GameObject canvasMoneyObj;
        [SerializeField] private TextMeshProUGUI textCostMoney;

        private Node node; //Случайная нода в зависимости от того на какой мы сейча локации
        private NodeData nodeData;

        private void Start()
        {
            nodeData = Resources.Load<NodeData>("Nodes/NodeData");
            node = nodeData.GetRandomNodeByWeightScene(100); //В параметры задавать шансы

            if (node == null)
                Destroy(gameObject);

            textName.text = node.nameNode;
            textCostMoney.text = $"{node.moneyCost}¤";
            playerTransform = FindFirstObjectByType<CharacterController>().transform;
        }

        private void Update()
        {
            if(playerTransform)
            {
                Vector3 direction = transform.position - playerTransform.position;
                direction.y = 0;

                if (direction != Vector3.zero)
                {
                    canvasMoneyObj.transform.rotation = Quaternion.LookRotation(direction);
                    canvasObj.transform.rotation = Quaternion.LookRotation(direction);
                }
            }
        }

        public Node GetNode()
        {
            return node;
        }
    }
}
