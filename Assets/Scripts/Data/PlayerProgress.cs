// using System;
// using System.Collections.Generic;

// [Serializable]
// public class PlayerProgress
// {
//     public int currentSemester = 1;
//     public int totalStars = 0;

//     public List<string> completedLabs = new List<string>();
//     public List<string> completedQuests = new List<string>();

//     public PlayerProgress() { }

//     public void AddStars(int stars)
//     {
//         totalStars += stars;
//     }

//     public bool HasCompletedLab(string labName)
//     {
//         return completedLabs.Contains(labName);
//     }

//     public void MarkLabCompleted(string labName)
//     {
//         if (!completedLabs.Contains(labName))
//             completedLabs.Add(labName);
//     }

//     public void MarkQuestCompleted(string questName)
//     {
//         if (!completedQuests.Contains(questName))
//             completedQuests.Add(questName);
//     }
// }

using System;
using System.Collections.Generic;

[Serializable]
public class PlayerProgress
{
    // --- Core Progress Data ---
    public int currentSemester = 1;     // Starts from Semester 1
    public int totalStars = 0;           // Total accumulated stars

    // --- Completion Tracking ---
    public List<string> completedLabs = new List<string>();
    public List<string> completedQuests = new List<string>();

    // --- Constructor ---
    public PlayerProgress() { }

    // --- Star System ---
    public void AddStars(int stars)
    {
        totalStars += stars;
    }

    public bool HasEnoughStars(int requiredStars)
    {
        return totalStars >= requiredStars;
    }

    // --- Semester Unlock System ---
    public void UnlockNextSemester()
    {
        currentSemester++;
    }

    public bool IsSemesterUnlocked(int semester)
    {
        return semester <= currentSemester;
    }

    // --- Lab Completion ---
    public bool HasCompletedLab(string labName)
    {
        return completedLabs.Contains(labName);
    }

    public void MarkLabCompleted(string labName)
    {
        if (!completedLabs.Contains(labName))
            completedLabs.Add(labName);
    }

    // --- Quest Completion ---
    public bool HasCompletedQuest(string questName)
    {
        return completedQuests.Contains(questName);
    }

    public void MarkQuestCompleted(string questName)
    {
        if (!completedQuests.Contains(questName))
            completedQuests.Add(questName);
    }

    // --- Reset Player Data (for testing/debugging) ---
    public void ResetProgress()
    {
        currentSemester = 1;
        totalStars = 0;
        completedLabs.Clear();
        completedQuests.Clear();
    }
}
