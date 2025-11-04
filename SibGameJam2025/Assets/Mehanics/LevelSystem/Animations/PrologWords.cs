using BulletSystem;
using UnityEngine;

public class PrologWords : MonoBehaviour
{
    [SerializeField] private Animation _remove;
    [SerializeField] private Collider _collider;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController b))
        {
            _remove.Play();
            _collider.enabled = false;
        }
    }
}
