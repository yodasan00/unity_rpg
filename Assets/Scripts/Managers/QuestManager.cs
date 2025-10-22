using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [Header("All Quests (Assign in Inspector)")]
    public List<QuestData> allQuests;

    [Header("Active Quests")]
    public List<QuestData> activeQuests = new List<QuestData>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Assign new daily quests
    public void AssignDailyQuests(int dailyCount)
    {
        activeQuests.Clear();
        List<QuestData> dailyQuests = allQuests.FindAll(q => q.isDaily);

        for (int i = 0; i < Mathf.Min(dailyCount, dailyQuests.Count); i++)
        {
            QuestData quest = Instantiate(dailyQuests[i]);
            quest.currentProgress = 0; // reset progress
            activeQuests.Add(quest);
        }

        Debug.Log($"Assigned {activeQuests.Count} daily quests.");
    }

    // Update progress for a quest
    public void UpdateQuestProgress(string questID, int amount = 1)
    {
        QuestData quest = activeQuests.Find(q => q.questID == questID);
        if (quest == null) return;

        quest.currentProgress += amount;
        if (quest.currentProgress >= quest.requiredAmount)
        {
            CompleteQuest(quest);
        }
        else
        {
            Debug.Log($"Quest progress: {quest.questName} {quest.currentProgress}/{quest.requiredAmount}");
        }
    }

    // Complete quest and give rewards
    private void CompleteQuest(QuestData quest)
    {
        activeQuests.Remove(quest);
        ProgressManager.Instance.AddStars(quest.rewardStars);
        Debug.Log($"Quest Completed: {quest.questName} | Reward: {quest.rewardStars} stars");
    }
}
