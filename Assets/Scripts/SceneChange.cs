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
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player entered doorway. Starting fade...");

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("Scene_change: No scene name specified! Scene change aborted.");
            return; // ✅ Prevents freezing
        }

        // Save next spawn point
        PlayerSpawnManager.nextSpawnPoint = spawnPointName;

        // Use FadeManager if present
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
