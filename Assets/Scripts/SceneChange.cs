using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_change : MonoBehaviour
{
    [Header("Scene Info")]
    [SerializeField] private string sceneName;          // Scene to load
    [SerializeField] private string spawnPointName;     // Spawn point in target scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered doorway. Starting fade...");

            // Save the next spawn point (for PlayerSpawnManager)
            PlayerSpawnManager.nextSpawnPoint = spawnPointName;

            // Use the global fade manager to fade + load
            if (FadeManager.Instance != null)
            {
                FadeManager.Instance.FadeToScene(sceneName);
            }
            else
            {
                Debug.LogWarning("No FadeManager found — loading scene instantly.");
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
