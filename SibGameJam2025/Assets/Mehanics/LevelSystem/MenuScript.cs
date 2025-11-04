using UnityEngine;

namespace LevelSystem
{
    public class MenuScript : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;

        private void Update()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                if(Time.timeScale > 0)
                {
                    if(!canvas.gameObject.activeSelf)
                        canvas.gameObject.SetActive(true);
                    else
                        canvas.gameObject.SetActive(false);
                }
            }
        }

        public void Quite()
        {
            Application.Quit();
        }
    }
}
