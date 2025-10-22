// using UnityEngine;

// [CreateAssetMenu(menuName = "Game/Lab Data", fileName = "NewLabData")]
// public class LabData : ScriptableObject
// {
//     [Header("Lab Info")]
//     public string labName;
//     public string description;
//     public int starReward = 5;

//     [Header("Gameplay Type")]
//     [Tooltip("Defines what kind of minigame or experiment this lab uses.")]
//     public LabType labType;

//     [Header("Question Set (optional)")]
//    // public QuestionSet questionSet;  // Step 4 will define this ScriptableObject

//     [Header("Scene Reference")]
//     public string labSceneName;  // The Unity scene name for this lab
// }

// public enum LabType
// {
//     ExperimentBased,
//     QuizBased,
//     SimulationBased
// }

// using UnityEngine;

// [CreateAssetMenu(fileName = "NewLab", menuName = "Lab System/LabData")]
// public class LabData : ScriptableObject
// {
//    public string labName;           // Name of the lab (Programming, Chemistry, etc.)
//   //  public string labSceneName;      // Scene name for the lab (optional)
//     public int starReward;           // Stars earned when completed
//     public QuestionSet questionSet;  // Drag your QuestionSet asset here
// }


using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewLab", menuName = "Lab System/LabData")]
public class LabData : ScriptableObject
{
    public string labName;          // e.g., "Programming Lab"
    public string subjectName;
    public LabType labType;
    public int starReward;
    public QuestionSet questionSet;

    [Header("Chemistry Experiments")]
    public List<ExperimentEntry> chemistryExperiments;

    // Helper property for unique key per semester
    public string GetUniqueKey(int semester)
    {
        return $"Semester{semester}_{labName}_{subjectName}";
    }
}

[System.Serializable]
public class ExperimentEntry
{
    public string experimentName;            // e.g., "Titration"
    public GameObject experimentPrefab;      // Prefab for drag-drop experiment
    public QuestionSet followUpQuestions;    // Optional follow-up questions
}


public enum LabMode
{
    MCQ,
    Typing,
    DragAndDrop,
    LogicConnection
}

public enum LabType
{
    Computer,
    Chemistry,
}