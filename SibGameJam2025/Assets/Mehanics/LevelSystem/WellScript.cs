using PlayerSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelSystem
{
    public class WellScript : MonoBehaviour
    {
        public GameObject canvas;
        public TextMeshProUGUI textFountainSouls;
        public Slider sliderFountainSouls;

        Transform playerTransform;
        private void Start()
        {
            playerTransform = FindFirstObjectByType<PlayerRefs>().transform;
        }

        private void Update()
        {
            if (playerTransform)
            {
                Vector3 direction = playerTransform.position - canvas.transform.position;
                direction.y = 0;

                if (direction != Vector3.zero)
                {
                    canvas.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180, 0);
                }
                print("1");
            }
        }
    }
}
