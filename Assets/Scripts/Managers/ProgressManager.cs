// using UnityEngine;
// using System.IO;

// public class ProgressManager : MonoBehaviour
// {
//     public static ProgressManager Instance { get; private set; }
//     public PlayerProgress Player { get; private set; }

//     private string savePath;

//     void Awake()
//     {
//         // Singleton pattern
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//             return;
//         }

//         savePath = Path.Combine(Application.persistentDataPath, "player_progress.json");
//         LoadProgress();
//     }

//     public void SaveProgress()
//     {
//         string json = JsonUtility.ToJson(Player, true);
//         File.WriteAllText(savePath, json);
//         Debug.Log("Progress saved: " + savePath);
//     }

//     public void LoadProgress()
//     {
//         if (File.Exists(savePath))
//         {
//             string json = File.ReadAllText(savePath);
//             Player = JsonUtility.FromJson<PlayerProgress>(json);
//             Debug.Log("Progress loaded.");
//         }
//         else
//         {
//             Player = new PlayerProgress();
//             SaveProgress();
//             Debug.Log("New progress created.");
//         }
//     }

//     public void ResetProgress()
//     {
//         Player = new PlayerProgress();
//         SaveProgress();
//     }
// }

using System.IO;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance { get; private set; }

    private string savePath;
    public PlayerProgress playerProgress;

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "playerProgress.json");
        LoadProgress();
    }

    // --- Save/Load ---
    public void SaveProgress()
    {
        string json = JsonUtility.ToJson(playerProgress, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Progress saved: " + savePath);
    }

    public void LoadProgress()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            playerProgress = JsonUtility.FromJson<PlayerProgress>(json);
            Debug.Log("Progress loaded successfully!");
        }
        else
        {
            playerProgress = new PlayerProgress();
            SaveProgress();
            Debug.Log("No save found, new progress created.");
        }
    }

    // --- Progress Modification ---
    public void AddStars(int stars)
    {
        playerProgress.AddStars(stars);
        SaveProgress();
    }

    public void CompleteLab(string labName)
    {
        playerProgress.MarkLabCompleted(labName);
        SaveProgress();
    }

    public void CompleteQuest(string questName)
    {
        playerProgress.MarkQuestCompleted(questName);
        SaveProgress();
    }

    public bool HasCompletedLab(string labName)
    {
        return playerProgress.HasCompletedLab(labName);
    }

    public int GetTotalStars()
    {
        return playerProgress.totalStars;
    }

    public int GetCurrentSemester()
    {
        return playerProgress.currentSemester;
    }
}
