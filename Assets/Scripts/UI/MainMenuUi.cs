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


using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenuUI : MonoBehaviour
{
    [Header("Scene Settings")]
    public string campusSceneName = "Rooms";

    [Header("Optional Fade")]
    public bool useFadeTransition = true;

    [Header("UI References")]
    [SerializeField] private GameObject Settings;
    [SerializeField] private GameObject Credits;
    [SerializeField] private Button loadButton; 

    public static MainMenuUI Instance { get; private set; }
    private bool settin = false; 
    private bool credit = false;
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
            Debug.Log(Application.persistentDataPath);

        }
    }

public void StartGame()
{
    Debug.Log("Starting NEW GAME...");
    if (HUDManager.Instance != null)
        Destroy(HUDManager.Instance.gameObject);

    ProgressManager.Instance.CreateNewGame();
    QuestManager.Instance.ResetDailyQuests();

    LoadScene();
}


   public void LoadGame()
{
    Debug.Log("Loading EXISTING GAME...");

    ProgressManager.Instance.LoadExistingGame();

    LoadScene();
}


    private void LoadScene()
    {
        // if (useFadeTransition && FadeManager.Instance != null)
        //     FadeManager.Instance.FadeToScene(campusSceneName);
        // else
        //     SceneManager.LoadScene(campusSceneName);
        FindAnyObjectByType<LoadingScreen>().LoadScene(campusSceneName);
    }
     public void ToggleSettings()
    {
        settin = !settin;
        Settings.SetActive(settin);
    }

    public void ToggleCredits()
    {
        credit = !credit;
        Credits.SetActive(credit);
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
