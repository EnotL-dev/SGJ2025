using UnityEngine;

namespace BattleSystem
{
    public class CameraRaycastTrack : MonoBehaviour
    {
        [SerializeField] private SpellCraft spellCraft;

        private LayerMask itemLayer = 1 << 7;
        private float rayDistance = 2.5f;

        private Camera _camera;

        private void Start()
        {
            _camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E)) // Левая кнопка мыши
            {
                ShootRaycast();
            }
        }

        private void ShootRaycast()
        {
            if (_camera == null) return;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayDistance, itemLayer))
            {
                Debug.Log($"Найден предмет: {hit.collider.name}");
                PickUpItem(hit.collider.GetComponent<ItemNode>().GetNode());
                Destroy(hit.collider.gameObject);
            }
        }

        private void PickUpItem(Node itemNode)
        {
            spellCraft.InitCraft(itemNode);
        }
    }
}
