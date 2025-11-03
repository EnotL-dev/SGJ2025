using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemySystem
{
    public class MeshRendererSelector : MonoBehaviour
    {
        [ContextMenu("Activate Meshes")]
        public void ActivateMeshes()
        {
            ChangeMeshes(true);
        }

        [ContextMenu("Deactivate Meshes")]
        public void DeactivateMeshes()
        {
            ChangeMeshes(false);
        }

        public void ChangeMeshes(bool value)
        {
            Transform[] allChildrenTransforms = GetComponentsInChildren<Transform>(true);
            List<GameObject> childGameObjects = allChildrenTransforms
            .Where(t => t != transform)
            .Select(t => t.gameObject)
            .ToList();
            foreach (GameObject child in childGameObjects)
            {
                if (child.TryGetComponent(out MeshRenderer mesh))
                {
                    mesh.enabled = value;
                }
            }
        }

    }
}