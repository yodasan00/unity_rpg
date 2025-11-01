using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    [Header("Fade Settings")]
    [SerializeField] private CanvasGroup fadePanel;
    [SerializeField] private float fadeDuration = 1.0f;

    private bool isFading = false;

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Start with fade-in when first loaded
       // StartCoroutine(FadeIn());
    }

    // 🔹 Fade to a new scene with fade-out → load → fade-in
    public void FadeToScene(string sceneName)
    {
        if (!isFading)
            StartCoroutine(FadeAndSwitchScene(sceneName));
    }

    private IEnumerator FadeAndSwitchScene(string sceneName)
    {
        isFading = true;

        // Fade out
        yield return StartCoroutine(Fade(0f, 1f));

        // Load scene
        SceneManager.LoadScene(sceneName);

        // Wait one frame for scene to load before fading in
        yield return null;

        // Fade in
        yield return StartCoroutine(Fade(1f, 0f));

        isFading = false;
    }

    // 🔹 Generic fade coroutine (used for both in & out)
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        fadePanel.gameObject.SetActive(true);

        float timer = 0f;
        while (timer < fadeDuration)
        {
            fadePanel.alpha = Mathf.Lerp(startAlpha, endAlpha, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        fadePanel.alpha = endAlpha;

        // Hide panel if fully transparent
        if (endAlpha == 0f)
            fadePanel.gameObject.SetActive(false);
    }

    // 🔹 Fade-in at startup (optional)
    // private IEnumerator FadeIn()
    // {
    //     fadePanel.alpha = 1f;
    //     fadePanel.gameObject.SetActive(true);
    //     yield return StartCoroutine(Fade(1f, 0f));
    // }
}
