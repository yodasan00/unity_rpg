

using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewLab", menuName = "Lab System/LabData")]
public class LabData : ScriptableObject
{
    public string labName;        //Lab ko Nam
    public string subjectName;  // Subject ko Nam eg: same lab ma different subject like java,Ds 
    public LabType labType; // Problem debugging or mcq
    public int starReward;  //total start or the max star awareded
    public QuestionSet questionSet; //Question set for the lab

    [Header("Chemistry Experiments")]
    public List<ExperimentEntry> chemistryExperiments; //for chem

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