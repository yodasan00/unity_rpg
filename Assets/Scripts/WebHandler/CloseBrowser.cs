using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseBrowser : MonoBehaviour
{
    public void OnCloseButtonClick()
    {
        string lastScene = PlayerPrefs.GetString("LastScene", "MainScene");

        SceneManager.LoadScene(lastScene);
    }
}
