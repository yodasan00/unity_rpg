using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public GameObject panel;
    public Slider bar;

    public float loadTime = 3.0f;
    string nextScene;

    public void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        StartCoroutine(LoadSequence());
    }

    IEnumerator LoadSequence()
    {
        // 1. Show panel
        panel.SetActive(true);

        // 2. IMPORTANT: wait 1 frame so panel becomes visible
        yield return null;

        // 3. Fake loading bar
        float t = 0f;
        while (t < loadTime)
        {
            t += Time.deltaTime;
            bar.value = t / loadTime;
            yield return null;
        }

        // 4. Finally load scene
        SceneManager.LoadScene(nextScene);
    }
}
