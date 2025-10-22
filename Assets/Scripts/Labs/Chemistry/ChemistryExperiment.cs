// using UnityEngine;

// public abstract class ChemistryExperiment : MonoBehaviour
// {
//     public LabData labData;

//     // Called when player performs a correct action (drop chemical, heat, etc.)
//     public abstract void PerformAction(string chemicalName);

//     // Called when experiment is completed
//     public void CompleteExperiment(int stars)
//     {
//         ProgressManager.Instance.CompleteLab(labData, stars);
//         Debug.Log(labData.labName + " completed with " + stars + " stars!");
//     }
// }

using UnityEngine;

public class ChemistryExperiment : MonoBehaviour
{
    public LabData labData;                    // Assigned from LabManager or prefab
    // public QuestionSet followUpQuestions;      // Optional — for post-experiment MCQs
    public string experimentName;             

    private bool isCompleted = false;

    public void CompleteExperiment(int starsEarned)
    {
        if (isCompleted) return; // Prevent double completion
        isCompleted = true;

        if (labData != null && ProgressManager.Instance != null)
        {
            ProgressManager.Instance.CompleteLab(labData, starsEarned);
            Debug.Log($"{labData.labName} ({experimentName}) completed with {starsEarned} stars!");
        }
        else
        {
            Debug.LogWarning("⚠️ Missing LabData or ProgressManager instance!");
        }


        // 🔹 Notify LabManager that the experiment is done
        if (LabManager.Instance != null)
        {
            LabManager.Instance.CompleteLab(starsEarned);
        }

        OnExperimentCompleted();
    }

    protected virtual void OnExperimentCompleted()
    {
        Debug.Log($"{experimentName} experiment finished!");
    }
}
