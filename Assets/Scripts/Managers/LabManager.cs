
// using UnityEngine;

// public class LabManager : MonoBehaviour
// {
//     public static LabManager Instance { get; private set; }
//     public LabData CurrentLab { get; private set; }

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     public void StartLab(LabData lab)
//     {
//         if (ProgressManager.Instance.HasCompletedLab(lab))
//         {
//             Debug.LogWarning("You already completed this lab this semester!");
//             return;
//         }

//         CurrentLab = lab;


//         switch (lab.labType) // for adding labs later since my early version has menumanager only for computer lab i have added only the chem lab here and later will add everthing in this logic
//         {
       
//         case LabType.Chemistry:
//                 if (lab.chemistryExperiments != null && lab.chemistryExperiments.Count > 0)
//                 {
//                     int index = Random.Range(0, lab.chemistryExperiments.Count);
//                     var entry = lab.chemistryExperiments[index];

//                     GameObject instance = Instantiate(entry.experimentPrefab);
//                     ChemistryExperiment experiment = instance.GetComponent<ChemistryExperiment>();
//                     experiment.labData = lab;
//                     experiment.experimentName = entry.experimentName;
                    
//                 }
//                 break;

//     }

//     }

//     public void CompleteLab(int starsEarned)
//     {
//         if (CurrentLab == null) return;

//         ProgressManager.Instance.CompleteLab(CurrentLab, starsEarned);
//         CurrentLab = null;
//     }
// }


// //used to manage the labs and their states

using UnityEngine;

public class LabManager : MonoBehaviour
{
    public static LabManager Instance { get; private set; }
    public LabData CurrentLab { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLab(LabData lab)
    {
        var progress = ProgressManager.Instance.playerProgress;

        // 🔹 Check if lab already completed today
        if (progress.HasCompletedLabToday(lab))
        {
            Debug.LogWarning("You’ve already done this lab today! Try again tomorrow.");
            return;
        }

        // 🔹 If lab completed in semester but not today, allow replay (optional)
        if (progress.HasCompletedLab(lab) && !progress.HasCompletedLabToday(lab))
        {
            Debug.Log("Replaying previously completed lab. You won’t earn new stars.");
            // You could set a flag here to prevent stars in CompleteLab()
            CurrentLab = lab;
        }
        else if (!progress.HasCompletedLab(lab))
        {
            // Normal first-time attempt
            CurrentLab = lab;
        }
        else
        {
            Debug.LogWarning("Unexpected state: lab already completed and flagged for today.");
            return;
        }

        // 🔹 Load the lab gameplay
        switch (lab.labType)
        {
            case LabType.Chemistry:
                if (lab.chemistryExperiments != null && lab.chemistryExperiments.Count > 0)
                {
                    int index = Random.Range(0, lab.chemistryExperiments.Count);
                    var entry = lab.chemistryExperiments[index];

                    GameObject instance = Instantiate(entry.experimentPrefab);
                    ChemistryExperiment experiment = instance.GetComponent<ChemistryExperiment>();
                    experiment.labData = lab;
                    experiment.experimentName = entry.experimentName;
                }
                break;
        }
    }

    public void CompleteLab(int starsEarned)
    {
        if (CurrentLab == null) return;

        var progress = ProgressManager.Instance.playerProgress;

        //If the lab was already completed in this semester, don’t give stars again
        bool firstCompletion = !progress.HasCompletedLab(CurrentLab);
        int starsToGive = firstCompletion ? starsEarned : 0;

        ProgressManager.Instance.CompleteLab(CurrentLab, starsToGive);
        Debug.Log(firstCompletion
            ? $"Lab completed for the first time! +{starsToGive} stars."
            : "Lab replay completed — no new stars added.");

        CurrentLab = null;
    }
}
