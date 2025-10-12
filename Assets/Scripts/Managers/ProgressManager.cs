using UnityEngine;
using System.IO;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance { get; private set; }
    public PlayerProgress Player { get; private set; }

    private string savePath;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        savePath = Path.Combine(Application.persistentDataPath, "player_progress.json");
        LoadProgress();
    }

    public void SaveProgress()
    {
        string json = JsonUtility.ToJson(Player, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Progress saved: " + savePath);
    }

    public void LoadProgress()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            Player = JsonUtility.FromJson<PlayerProgress>(json);
            Debug.Log("Progress loaded.");
        }
        else
        {
            Player = new PlayerProgress();
            SaveProgress();
            Debug.Log("New progress created.");
        }
    }

    public void ResetProgress()
    {
        Player = new PlayerProgress();
        SaveProgress();
    }
}
