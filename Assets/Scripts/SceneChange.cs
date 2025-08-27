// using UnityEngine;
// using UnityEngine.SceneManagement;
// public class Scene_change : MonoBehaviour
// {
//     [SerializeField] private string sceneName;
//     public void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             Debug.Log("Player has entered the doorway. Loading " + sceneName + "...");
//             SceneManager.LoadScene(sceneName); 
//         }
//     }
// }

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_change : MonoBehaviour
{
    
    [SerializeField] private string sceneName;

    
    [SerializeField] private CanvasGroup fadePanel;

    
    [SerializeField] private float fadeDuration = 1.0f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the doorway. Starting fade...");
           
            StartCoroutine(FadeOutAndLoad(sceneName)); 
        }
    }

    
    private IEnumerator FadeOutAndLoad(string sceneToLoad)
    {
        
        fadePanel.gameObject.SetActive(true);
        fadePanel.alpha = 0;
        float timer = 0f;
        while (timer < fadeDuration)
        {
            fadePanel.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null; 
        }

        fadePanel.alpha = 1;

        Debug.Log("Fade complete. Loading " + sceneToLoad + "...");
        SceneManager.LoadScene(sceneToLoad);
    }
}