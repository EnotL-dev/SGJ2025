using UnityEngine;

namespace PlayerSystem
{
    public class KillPlayerZone : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMaskHit;

        private void OnTriggerEnter(Collider other)
        {
            if (IsLayerInMask(other.gameObject.layer, _layerMaskHit))
            {
                if (other.gameObject.TryGetComponent(out Health health))
                {
                    Debug.Log("<color=red>Игрок мгновенно убит</color>");
                    health.KillImmediately();
                }
            }
        }

        public static bool IsLayerInMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}
