using UnityEngine;

public class SpellCollision : MonoBehaviour
{
    [HideInInspector] public int damage;
    [SerializeField] private LayerMask _layerMaskHit;
    [SerializeField] private LayerMask _layerMaskIgnore;

    private void OnTriggerEnter(Collider other)
    {
        if (IsLayerInMask(other.gameObject.layer, _layerMaskIgnore))
            return;
        if (IsLayerInMask(other.gameObject.layer, _layerMaskHit))
        {
            if (other.gameObject.TryGetComponent(out Health health))
            {
                health.Reduce(damage);
            }
        }

        Destroy(gameObject);
    }

    public static bool IsLayerInMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
