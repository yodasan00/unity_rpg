
using System;
using System.Collections.Generic;

[Serializable]
public class PlayerProgress
{
    public int currentSemester = 1;
    public int totalStars = 0;

    // Track labs per semester+subject
    public List<string> completedLabs = new List<string>();
    public List<string> completedQuests = new List<string>();

    public void AddStars(int stars)
    {
        totalStars += stars;
    }

    public void UnlockNextSemester()
    {
        currentSemester++;
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

    public void MarkLabCompleted(LabData lab)
    {
        string key = lab.GetUniqueKey(currentSemester);
        if (!completedLabs.Contains(key))
            completedLabs.Add(key);
    }

    public bool HasCompletedQuest(string questName)
    {
        return completedQuests.Contains(questName);
    }

    public void MarkQuestCompleted(string questName)
    {
        if (!completedQuests.Contains(questName))
            completedQuests.Add(questName);
    }

    public void ResetProgress()
    {
        currentSemester = 1;
        totalStars = 0;
        completedLabs.Clear();
        completedQuests.Clear();
    }
}
