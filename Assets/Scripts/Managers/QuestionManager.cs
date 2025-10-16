using UnityEngine;
using System.Collections.Generic;

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager Instance;

    [Header("All Labs")]
    public List<LabData> allLabs; // Assign all LabData assets in Inspector

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>
    /// Returns the QuestionSet for a lab based on its name.
    /// </summary>
    public QuestionSet GetQuestionsForLab(string labName)
    {
        foreach (var lab in allLabs)
        {
            if (lab.labName == labName)
            {
                if (lab.questionSet != null)
                    return lab.questionSet;
                else
                {
                    Debug.LogError($"Lab '{labName}' has no QuestionSet assigned!");
                    return null;
                }
            }
        }

        Debug.LogError($"Lab '{labName}' not found in QuestionManager!");
        return null;
    }

    }

