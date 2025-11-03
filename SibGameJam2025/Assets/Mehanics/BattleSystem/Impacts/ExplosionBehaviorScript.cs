using SaveSystem;
using UnityEngine;

namespace BattleSystem
{
    public class ExplosionBehaviorScript : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerDamaging;
        private void OnTriggerEnter(Collider other)
        {
            if (!IsLayerInMask(other.gameObject.layer, _layerDamaging))
                return;
            else
            {
                if (other.gameObject.TryGetComponent(out Health health))
                {
                    //health.Reduce(2*(SaveData.TempData.playerParams.bonusDamage-1));
                }
            }
        }

        public static bool IsLayerInMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}