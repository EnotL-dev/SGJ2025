using UnityEngine;

namespace BulletSystem
{
    public class BulletCollision : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private Bullet _bullet;
        [SerializeField] private LayerMask _layerMaskHit;
        [SerializeField] private LayerMask _layerMaskIgnore;
        [SerializeField] private AudioSource _audio;
        private void OnTriggerEnter(Collider other)
        {
            if (IsLayerInMask(other.gameObject.layer, _layerMaskIgnore))
                return;
            if (IsLayerInMask(other.gameObject.layer, _layerMaskHit))
            {
                if (other.gameObject.TryGetComponent(out Health health))
                {
                    health.Reduce(_damage);
                    _audio.Play();
                }
            }
            _bullet.Hide();
        }

        public static bool IsLayerInMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}