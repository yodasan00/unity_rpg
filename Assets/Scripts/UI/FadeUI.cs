using UnityEngine;
using TMPro;
using System.Collections;
using Unity.VisualScripting;

public class FadeUI : MonoBehaviour
{
    [Header("UI References")]
    public CanvasGroup fadePanel;         // The black panel
    public TextMeshProUGUI nextDayText;   // The "Next Day..." text

    [Header("Timing")]
    public float fadeDuration = 1f;       // Time to fade in/out
    public float textFadeDelay = 0.5f;    // Delay before text appears
    public float textFadeDuration = 0.5f; // How fast text fades in/out
    public float stayDuration = 2f;       // How long the fade stays before returning

    void Start()
    {
        // Initialize UI elements
        fadePanel.alpha = 0f;
        Color textColor = nextDayText.color;
        textColor.a = 0f;
        nextDayText.color = textColor;
    }
    public IEnumerator FadeSequence()
    {
        // Step 1: Fade screen to black
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 0f, 1f, fadeDuration));

        // Step 2: Wait a moment before showing text
        yield return new WaitForSecondsRealtime(textFadeDelay);

        // Step 3: Fade text in
        yield return StartCoroutine(FadeTextAlpha(nextDayText, 0f, 1f, textFadeDuration));

        // Step 4: Hold text visible for stayDuration
        yield return new WaitForSecondsRealtime(stayDuration);

        // Step 5: Fade text out
        yield return StartCoroutine(FadeTextAlpha(nextDayText, 1f, 0f, textFadeDuration));

        // Step 6: Fade screen back to normal
        yield return StartCoroutine(FadeCanvasGroup(fadePanel, 1f, 0f, fadeDuration));
    }

    IEnumerator FadeCanvasGroup(CanvasGroup group, float from, float to, float duration)
    {
        float t = 0f;
        group.alpha = from;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            group.alpha = Mathf.Lerp(from, to, t / duration);
            yield return null;
        }
        group.alpha = to;
    }

    IEnumerator FadeTextAlpha(TextMeshProUGUI text, float from, float to, float duration)
    {
        float t = 0f;
        Color color = text.color;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(from, to, t / duration);
            text.color = color;
            yield return null;
        }
        color.a = to;
        text.color = color;
    }
}
