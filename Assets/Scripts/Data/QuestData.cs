using UnityEngine;
public enum QuestType
{
    AttendLab,
    TalkToNPC,
    PlayTableTennis,
    AttendLecture
}

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class QuestSO : ScriptableObject
{
    public string questName;
    public QuestType questType;
    public string description;
    public bool isCompleted;
    public int rewardStars;
}
