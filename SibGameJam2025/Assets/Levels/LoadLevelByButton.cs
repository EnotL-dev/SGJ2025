using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelByButton : MonoBehaviour
{
    public void LoadLevel(int indexScene) // 0 - default
    {
        PlayerPrefs.DeleteAll();
        Application.targetFrameRate = 60;
        Debug.Log("Частота задана 60");
        SceneManager.LoadScene(indexScene);
    }
}
