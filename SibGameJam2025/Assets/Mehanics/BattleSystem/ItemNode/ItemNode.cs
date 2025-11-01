using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem
{
    public class ItemNode : MonoBehaviour
    {
        [SerializeField] private GameObject canvasObj;

        private Node node; //Случайная нода в зависимости от того на какой мы сейча локации
        private NodeData nodeData;

        private void Start()
        {
            nodeData = Resources.Load<NodeData>("Nodes/NodeData");
            node = nodeData.GetRandomNodeByWeightScene(100);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.LogWarning(node.nameNode);
            }
        }
    }
}
