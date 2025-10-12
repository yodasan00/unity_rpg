using System;
using System.Collections.Generic;

[Serializable]
public class PlayerProgress
{
    public int currentSemester = 1;
    public int totalStars = 0;

    public List<string> completedLabs = new List<string>();
    public List<string> completedQuests = new List<string>();

    public PlayerProgress() { }

    public void AddStars(int stars)
    {
        totalStars += stars;
    }

    public bool HasCompletedLab(string labName)
    {
        return completedLabs.Contains(labName);
    }

    public void MarkLabCompleted(string labName)
    {
        if (!completedLabs.Contains(labName))
            completedLabs.Add(labName);
    }

    public void MarkQuestCompleted(string questName)
    {
        if (!completedQuests.Contains(questName))
            completedQuests.Add(questName);
    }
}

