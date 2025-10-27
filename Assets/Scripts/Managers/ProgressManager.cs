

using System.IO;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager Instance { get; private set; }

    public PlayerProgress playerProgress;
    private string savePath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "playerProgress.json");
        ResetProgress();
        LoadProgress();
    }

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

    public void AddStars(int stars)
    {
        playerProgress.AddStars(stars);
        SaveProgress();
        SemesterManager.Instance.TryUnlockNextSemester();
    }

    public void CompleteLab(LabData lab, int earnedStars)
{
    if (!playerProgress.HasCompletedLab(lab))
    {
        playerProgress.MarkLabCompleted(lab);

        int starsToAdd = Mathf.Min(earnedStars, lab.starReward);
        AddStars(starsToAdd);

        Debug.Log($" Lab completed: {lab.labName} - {lab.subjectName}, Stars Earned: {starsToAdd}");
    }
}


    public bool HasCompletedLab(LabData lab)
    {
        return playerProgress.HasCompletedLab(lab);
    }

    public void ResetProgress()
    {
        playerProgress.ResetProgress();
        SaveProgress();
    }
}

//manages player progress including saving/loading and tracking completed labs and stars