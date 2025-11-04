using PlayerSystem;
using UnityEngine;

namespace EnemySystem.Boss
{
    public class ExplosionTarget : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private int _damage;
        [SerializeField] private GameObject _image;

        public void ActivateExplosion()
        {
            _image.SetActive(false);
            if (Vector3.Distance(transform.position, PlayerRefs.Instance.CharacterController.transform.position) < _distance)
                if (PlayerRefs.Instance.CharacterController.TryGetComponent(out Health health))
                {
                    health.Reduce(_damage);
                }
        }

        public void DeactivateExplosion()
        {
            _image.SetActive(true);
        }
    }
}