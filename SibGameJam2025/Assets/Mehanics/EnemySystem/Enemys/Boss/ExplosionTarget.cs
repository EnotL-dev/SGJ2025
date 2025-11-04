using BattleSystem;
using PlayerSystem;
using Unity.VisualScripting;
using UnityEngine;

namespace EnemySystem.Boss
{
    public class ExplosionTarget : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private int _damage;
        [SerializeField] private GameObject _image;
        [SerializeField] private AudioSource _sound;
        [SerializeField] private ParticleSystem _particleSystem;

        public void ActivateExplosion()
        {
            _sound.Play();
            _particleSystem.Play();
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