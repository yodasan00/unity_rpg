// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class SceneSwitcher : MonoBehaviour
// {
//     // 💡 ACTION: Set this to the unique URL in the Inspector for each button/purpose
//     [Header("Browser Destination")]
//     public string initialURL = "https://www.your-first-use.com"; 

//     // 💡 ACTION: Set this to the name of your browser scene
//     [Header("Scene Name")]
//     public string browserSceneName = "Web"; 

//     public void LoadBrowserSceneWithNewURL()
//     {
//         // 1. Pass the unique URL data
//         SceneDataLoader.SetNextBrowserURL(initialURL);
        
//         // 2. Load the reusable browser scene
//         SceneManager.LoadScene(browserSceneName);
//     }
// }

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [Header("Browser Destination")]
    public string initialURL = "https://www.your-first-use.com";

    [Header("Scene Name")]
    public string browserSceneName = "Web";

    public void LoadBrowserSceneWithNewURL()
    {
        // 1️⃣ Save the current scene name
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        // 2️⃣ Pass the URL data
        SceneDataLoader.SetNextBrowserURL(initialURL);

        // 3️⃣ Load the browser scene
        SceneManager.LoadScene(browserSceneName);
    }
}
