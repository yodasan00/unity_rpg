using UnityEngine;
using System.Collections; // Needed for IEnumerator
using VoltstroStudios.UnityWebBrowser;

public class BrowserURLInitializer : MonoBehaviour
{
    // Ensure this is linked to the WebBrowserUIBasic component in the Inspector!
    public WebBrowserUIBasic browserUI; 

    // The amount of time to wait before sending the URL command
    private const float NavigationDelay = 0.5f; // 0.5 seconds should be safe

    private void Start()
    {
        if (browserUI == null)
        {
            Debug.LogError("WebBrowserUIBasic reference is missing! Cannot start navigation.");
            return;
        }

        // Start the coroutine instead of immediately calling navigation
        StartCoroutine(DelayedNavigation());
    }

    private IEnumerator DelayedNavigation()
    {
        // 1. Wait for a short, fixed duration to ensure the browser has fully settled.
        yield return new WaitForSeconds(NavigationDelay); 

        // 2. Retrieve the URL passed by the previous scene
        string urlToLoad = SceneDataLoader.InitialBrowserURL;
        
        if (string.IsNullOrEmpty(urlToLoad) || urlToLoad == "about:blank")
        {
            Debug.LogWarning("No specific URL set. Using the browser's default.");
            yield break; // Stop the coroutine
        }
        
        // 3. Force the navigation to the custom URL
        Debug.Log($"Delay complete. Forcing navigation to: {urlToLoad}");
        browserUI.NavigateUrl(urlToLoad); 
    }
}