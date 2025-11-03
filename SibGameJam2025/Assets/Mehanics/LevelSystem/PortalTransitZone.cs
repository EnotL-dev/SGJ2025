using PlayerSystem;
using UnityEngine;

namespace LevelSystem
{
    public class PortalTransitZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                //PlayerRefs.Instance.levelManager
            }
        }
    }
}
