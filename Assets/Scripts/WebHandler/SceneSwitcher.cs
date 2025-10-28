using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    // 💡 ACTION: Set this to the unique URL in the Inspector for each button/purpose
    [Header("Browser Destination")]
    public string initialURL = "https://www.your-first-use.com"; 

    // 💡 ACTION: Set this to the name of your browser scene
    [Header("Scene Name")]
    public string browserSceneName = "Web"; 

    public void LoadBrowserSceneWithNewURL()
    {
        // 1. Pass the unique URL data
        SceneDataLoader.SetNextBrowserURL(initialURL);
        
        // 2. Load the reusable browser scene
        SceneManager.LoadScene(browserSceneName);
    }
}