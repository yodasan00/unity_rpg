using UnityEngine;
using UnityEngine.SceneManagement;

public class TTExit : MonoBehaviour
{
    [SerializeField] private string spawnPointName;  
    public void ExitTableTennis()
    {
        Time.timeScale = 1f;
        string returnScene = PlayerPrefs.GetString("ReturnScene", "MainScene");
        PlayerSpawnManager.nextSpawnPoint = spawnPointName;
        SceneManager.LoadScene(returnScene);
    }
}

