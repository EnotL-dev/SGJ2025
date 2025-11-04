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
                if(Time.timeScale > 0.1)
                {
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    canvas.gameObject.SetActive(true);
                }
                else
                {
                    CloseMenu();
                }
            }
        }

        public void CloseMenu()
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                canvas.gameObject.SetActive(false);
            }
        }

        public void Quite()
        {
            Application.Quit();
        }
    }
}
