using PlayerSystem;
using UnityEngine;

namespace LevelSystem
{
    public class PortalTransitZone : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        private void OnTriggerEnter(Collider other)
        {
            if(IsLayerInMask(other.gameObject.layer, _layer))
            {
                PlayerRefs.Instance.levelManager.LoadNextLevel();
            }
        }

        public static bool IsLayerInMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}
