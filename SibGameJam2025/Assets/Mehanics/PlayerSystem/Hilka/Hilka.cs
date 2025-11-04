using UnityEngine;

public class Hilka : MonoBehaviour
{
    [SerializeField] private int _countRestore;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CharacterController player))
        {
            if (other.gameObject.TryGetComponent(out Health health))
            {
                health.Add(_countRestore);
                Destroy(gameObject);
            }
        }
    }


}
