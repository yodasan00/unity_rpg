
using System;
using System.Collections.Generic;

// [Serializable]
// public class PlayerProgress
// {
//     public int currentSemester = 1;
//     public int totalStars = 0;

//     // Track labs per semester+subject
//     public List<string> completedLabs = new List<string>();
//     public List<string> completedQuests = new List<string>();

//     public void AddStars(int stars)
//     {
//         totalStars += stars;
//     }

//     public void UnlockNextSemester()
//     {
//         currentSemester++;
//     }

//     public bool IsSemesterUnlocked(int semester)
//     {
//         return semester <= currentSemester;
//     }

//     public bool HasCompletedLab(LabData lab)
//     {
//         string key = lab.GetUniqueKey(currentSemester);
//         return completedLabs.Contains(key);
//     }

//     public void MarkLabCompleted(LabData lab)
//     {
//         string key = lab.GetUniqueKey(currentSemester);
//         if (!completedLabs.Contains(key))
//             completedLabs.Add(key);
//     }

//     public bool HasCompletedQuest(string questName)
//     {
//         return completedQuests.Contains(questName);
//     }

//     public void MarkQuestCompleted(string questName)
//     {
//         if (!completedQuests.Contains(questName))
//             completedQuests.Add(questName);
//     }

//     public void ResetProgress()
//     {
//         currentSemester = 1;
//         totalStars = 0;
//         completedLabs.Clear();
//         completedQuests.Clear();
//     }
// }

[Serializable]
public class PlayerProgress
{
    public int currentSemester = 1;
    public int totalStars = 0;
    public int currentDay = 1; // 🔹 NEW

    public string currentScene ; // 🔹 NEW — saves the scene
    public float playerPosX = 0f;               // 🔹 NEW — saves X position
    public float playerPosY = 0f;               // 🔹 NEW — saves Y position
    public float playerPosZ = 0f; 

    public List<string> completedLabs = new List<string>();
    public List<string> completedQuests = new List<string>();
    public List<string> dailyCompletedLabs = new List<string>(); // 🔹 NEW - Labs done today

    public void AddStars(int stars)
    {
        totalStars += stars;
        HUDManager.Instance.UpdateStars(totalStars);
    }

    public void UnlockNextSemester()
    {
        currentSemester++;
        HUDManager.Instance.UpdateSemester(currentSemester.ToString());
    }

    public bool IsSemesterUnlocked(int semester)
    {
        return semester <= currentSemester;
    }

    public bool HasCompletedLab(LabData lab)
    {
        string key = lab.GetUniqueKey(currentSemester);
        return completedLabs.Contains(key);
    }

    // public void MarkLabCompleted(LabData lab)
    // {
    //     string key = lab.GetUniqueKey(currentSemester);
    //     if (!completedLabs.Contains(key))
    //         completedLabs.Add(key);
    //     dailyCompletedLabs.Add(key); // 🔹 Mark for today
    // }

    public void MarkLabCompleted(LabData lab)
    {
    string key = lab.GetUniqueKey(currentSemester);
    if (!dailyCompletedLabs.Contains(key))
        dailyCompletedLabs.Add(key);
    }

    public void ResetDailyProgress()
    {
        dailyCompletedLabs.Clear(); // 🔹 Reset daily lab completions
    }

    public void NextDay()
    {
        currentDay++;
        ResetDailyProgress(); // 🔹 Clean slate for new day
    }

    public bool HasCompletedLabToday(LabData lab)
    {
        string key = lab.GetUniqueKey(currentSemester);
        return dailyCompletedLabs.Contains(key);
    }

   public void ResetProgress()
    {
        currentSemester = 1;
        totalStars = 0;
        currentDay = 1;
        completedLabs.Clear();
        completedQuests.Clear();
        dailyCompletedLabs.Clear();
        currentScene = "CampusScene";
        playerPosX = 0f;
        playerPosY = 0f;
        playerPosZ = 0f;
    }
}
