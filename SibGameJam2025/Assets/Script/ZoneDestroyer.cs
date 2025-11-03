using UnityEngine;

public class ZoneDestroyer : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMaskHit;
    [Space(5)]
    [SerializeField] private GameObject[] destroyObjs;
    private void OnTriggerEnter(Collider other)
    {
        if (IsLayerInMask(other.gameObject.layer, _layerMaskHit))
        {
            foreach(GameObject obj in destroyObjs)
            {
                Destroy(obj);
            }
        }
    }

    public static bool IsLayerInMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
