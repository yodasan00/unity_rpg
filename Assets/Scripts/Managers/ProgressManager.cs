

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

       
    }

    public void SaveProgress()
    {

           // Save scene name
    playerProgress.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
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

    public bool HasSaveFile()
{
    return File.Exists(savePath);
 }

public void CreateNewGame()
{
    playerProgress = new PlayerProgress();   // clean data
    SaveProgress();                          // save clean file
}

public void LoadExistingGame()
{
    if (File.Exists(savePath))
    {
        string json = File.ReadAllText(savePath);
        playerProgress = JsonUtility.FromJson<PlayerProgress>(json);
    }
    else
    {
        CreateNewGame();
    }
}



    public void AddStars(int stars)
    {
        playerProgress.AddStars(stars);
        // HUDManager.Instance.UpdateStars(stars);
        SaveProgress();
        SemesterManager.Instance.TryUnlockNextSemester();
    }

    // public void CompleteLab(LabData lab, int earnedStars)
    // {
    //     if (playerProgress.HasCompletedLabToday(lab))
    //     {
    //         Debug.Log("You have already done this lab today. Try again tomorrow!");
    //         return;
    //     }

    //     if (!playerProgress.HasCompletedLab(lab))
    //     {
    //         playerProgress.MarkLabCompleted(lab);
    //         int starsToAdd = Mathf.Min(earnedStars, lab.starReward);
    //         AddStars(starsToAdd);
    //         Debug.Log($"Lab completed: {lab.labName}, Stars: {starsToAdd}");
    //     }
    //     else
    //     {
    //         Debug.Log("Lab already completed in this semester.");
    //     }
    // }
public void CompleteLab(LabData lab, int earnedStars)
{
    if (playerProgress.HasCompletedLabToday(lab))
    {
        Debug.Log("You already did this lab today. Try again tomorrow!");
        return;
    }

    playerProgress.MarkLabCompleted(lab);

    int starsToAdd = Mathf.Min(earnedStars, lab.starReward);
    AddStars(starsToAdd);
    Debug.Log($"Lab completed: {lab.labName}, Stars Earned: {starsToAdd}");
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
    public void NextDay()
{
    playerProgress.NextDay();
    SaveProgress();
    Debug.Log("Moved to next day: " + playerProgress.currentDay);
    HUDManager.Instance.UpdateTime("Day " + playerProgress.currentDay);
    //HUDManager.Instance.Update(playerProgress.currentDay); // optional, if you have HUD
}

}



// //manages player progress including saving/loading and tracking completed labs and stars