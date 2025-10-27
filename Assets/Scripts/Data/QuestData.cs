//USed for Quest

using UnityEngine;

public enum QuestType { Lab, MCQ, Collection, Interaction }

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class QuestData : ScriptableObject
{
    public string questID;              // Unique ID
    public string questName;            // Display name
    [TextArea] public string description;   // Quest description
    public QuestType type;              // Type of quest
    public int requiredAmount;          // e.g., number of questions/labs/items
    public int rewardStars;             // Reward for completing quest
    public bool isDaily;                // Is this a daily quest?

    [HideInInspector] public int currentProgress; // Runtime progress
}
