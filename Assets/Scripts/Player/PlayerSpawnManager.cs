using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    public static string nextSpawnPoint; // Set before changing scene

    private void Start()
    {
        // Find and move player to spawn point if defined
        if (!string.IsNullOrEmpty(nextSpawnPoint))
        {
            GameObject spawn = GameObject.Find(nextSpawnPoint);
            if (spawn != null)
            {
                transform.position = spawn.transform.position;
            }
            else
            {
                Debug.LogWarning($"Spawn point '{nextSpawnPoint}' not found in this scene!");
            }
        }
    }
}
