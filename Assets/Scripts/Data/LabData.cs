using UnityEngine;

[CreateAssetMenu(menuName = "Game/Lab Data", fileName = "NewLabData")]
public class LabData : ScriptableObject
{
    [Header("Lab Info")]
    public string labName;
    public string description;
    public int starReward = 5;

    [Header("Gameplay Type")]
    [Tooltip("Defines what kind of minigame or experiment this lab uses.")]
    public LabType labType;

    [Header("Question Set (optional)")]
   // public QuestionSet questionSet;  // Step 4 will define this ScriptableObject

    [Header("Scene Reference")]
    public string labSceneName;  // The Unity scene name for this lab
}

public enum LabType
{
    ExperimentBased,
    QuizBased,
    SimulationBased
}
