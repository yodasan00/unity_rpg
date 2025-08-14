using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_change : MonoBehaviour
{
    [SerializeField] private string sceneName;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the doorway. Loading " + sceneName + "...");
            SceneManager.LoadScene(sceneName); 
        }
    }
}