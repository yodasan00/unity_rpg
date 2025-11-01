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
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void FadeToScene(string sceneName)
    {
        // ✅ Prevent coroutine if no scene name is valid
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("FadeManager: Attempted to fade to an empty or null scene name. Aborting.");
            return;
        }

        if (!isFading)
            StartCoroutine(FadeAndSwitchScene(sceneName));
    }

    private IEnumerator FadeAndSwitchScene(string sceneName)
    {
        isFading = true;

        // Fade out
        yield return StartCoroutine(Fade(0f, 1f));

        // ✅ Double-check before loading
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("FadeManager: Scene name was empty during fade transition. Aborting load.");
            isFading = false;
            yield break;
        }

        // Load scene
        SceneManager.LoadScene(sceneName);

        yield return null; // Wait one frame

        // Fade in
        yield return StartCoroutine(Fade(1f, 0f));

        isFading = false;
    }

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

        if (endAlpha == 0f)
            fadePanel.gameObject.SetActive(false);
    }
}
