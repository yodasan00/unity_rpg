using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Semester Data", fileName = "NewSemesterData")]
public class SemesterData : ScriptableObject
{
    [Header("Basic Info")]
    public string semesterName;
    [Tooltip("How many stars required to unlock this semester")]
    public int requiredStarsToUnlock;

    [Header("Content")]
    public List<LabData> labs;      
    // public List<QuestData> quests;  

    [TextArea] 
    public string description;
}
