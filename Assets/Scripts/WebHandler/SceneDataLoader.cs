using UnityEngine;

public class SceneDataLoader : MonoBehaviour
{
    public static string InitialBrowserURL { get; private set; } = "https://www.google.com"; // Default/Fallback

    private static SceneDataLoader _instance;

    [System.Obsolete]
    public static SceneDataLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneDataLoader>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("SceneDataLoader");
                    _instance = go.AddComponent<SceneDataLoader>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            // Keeps the data available after the scene changes
            DontDestroyOnLoad(gameObject); 
        }
        else if (_instance != this)
        {
            Destroy(gameObject); 
        }
    }

    // Call this method from any script to set the URL for the next scene load
    public static void SetNextBrowserURL(string url)
    {
        InitialBrowserURL = url;
    }
}