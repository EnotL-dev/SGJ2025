using UnityEngine;

namespace BulletSystem
{
    public class BulletCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CharacterController player))
            {

            }
        }
    }
}