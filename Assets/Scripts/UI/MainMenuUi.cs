using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene Settings")]
       public string campusSceneName = "Rooms";

    [Header("Optional Fade")]
    public bool useFadeTransition = true;

    public void StartGame()
    {
        Debug.Log("Starting game...");

        if (useFadeTransition && FadeManager.Instance != null)
        {
            FadeManager.Instance.FadeToScene(campusSceneName);
        }
        else
        {
            SceneManager.LoadScene(campusSceneName);
        }
    }

    public void OpenSettings()
    {
        Debug.Log("Opening settings...");
        // settingsPanel.SetActive(true);
    }

    public void Credits()
    {
        Debug.Log("Showing credits...");
        // creditsPanel.SetActive(true);
    }
    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

        // For testing inside Unity editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
