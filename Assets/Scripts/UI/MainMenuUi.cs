// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class MainMenuUI : MonoBehaviour
// {
//     [Header("Scene Settings")]
//        public string campusSceneName = "Rooms";

//     [Header("Optional Fade")]
//     public bool useFadeTransition = true;

//     public void StartGame()
//     {
//         Debug.Log("Starting game...");

//         if (useFadeTransition && FadeManager.Instance != null)
//         {
//             FadeManager.Instance.FadeToScene(campusSceneName);
//         }
//         else
//         {
//             SceneManager.LoadScene(campusSceneName);
//         }
//     }

//     public void OpenSettings()
//     {
//         Debug.Log("Opening settings...");
//         // settingsPanel.SetActive(true);
//     }

//     public void Credits()
//     {
//         Debug.Log("Showing credits...");
//         // creditsPanel.SetActive(true);
//     }
//     public void ExitGame()
//     {
//         Debug.Log("Exiting game...");
//         Application.Quit();

//         // For testing inside Unity editor
// #if UNITY_EDITOR
//         UnityEditor.EditorApplication.isPlaying = false;
// #endif
//     }
// }


using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene Settings")]
    public string campusSceneName = "Rooms";

    [Header("Optional Fade")]
    public bool useFadeTransition = true;

    [Header("UI References")]
    [SerializeField] private Button loadButton; // 👈 Drag your Load Game button here in Inspector

    private void Start()
    {
        // Disable the Load button if no save file exists
        if (!ProgressManager.Instance.HasSaveFile())
        {
            loadButton.interactable = false;
            Debug.Log("Load button disabled - no save file found.");
        }
        else
        {
            loadButton.interactable = true;
            Debug.Log("Save file found - Load button enabled.");
        }
    }

    public void StartGame()
    {
        Debug.Log("Starting new game...");
        ProgressManager.Instance.ResetProgress(); // start fresh
        LoadScene();
    }

    public void LoadGame()
    {
        Debug.Log("Loading existing save...");
        ProgressManager.Instance.LoadProgress();
        LoadScene();
    }

    private void LoadScene()
    {
        if (useFadeTransition && FadeManager.Instance != null)
            FadeManager.Instance.FadeToScene(campusSceneName);
        else
            SceneManager.LoadScene(campusSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
