using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevelByButton : MonoBehaviour
{
    public void LoadLevel(int indexScene) // 0 - default
    {
        SceneManager.LoadScene(indexScene);
    }
}
