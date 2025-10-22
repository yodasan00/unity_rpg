// using UnityEngine;
// using UnityEngine.SceneManagement;

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

//     // Called when player clicks a lab button
//     public void StartLab(LabData lab)
//     {
//         CurrentLab = lab;
//        // SceneManager.LoadScene(lab.labSceneName);
//     }

//     // Called from lab script when lab is complete
//     public void CompleteLab(int starsEarned)
//     {
//         if (CurrentLab == null) return;

//         var progress = ProgressManager.Instance.playerProgress;

//         // Mark lab completed
//         if (!progress.HasCompletedLab(CurrentLab.labName))
//         {
//             progress.MarkLabCompleted(CurrentLab.labName);
//             progress.AddStars(starsEarned); // Add earned stars
//             ProgressManager.Instance.SaveProgress();

//             Debug.Log($"Lab '{CurrentLab.labName}' completed! +{starsEarned} stars");
//         }

//         CurrentLab = null;

//         // Return to campus/main menu
//        // SceneManager.LoadScene("Campus");
//     }
// }

// using UnityEngine;
// using UnityEngine.SceneManagement;

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

//     // Called when player clicks a lab button
//     public void StartLab(LabData lab)
//     {
//         CurrentLab = lab;
//         Debug.Log($"Starting Lab: {lab.labName}");
//         // SceneManager.LoadScene(lab.labSceneName);
//     }

//     // Called when lab is completed
//     public void CompleteLab(int starsEarned)
//     {
//         if (CurrentLab == null) return;

//         var progress = ProgressManager.Instance.playerProgress;

//         // Mark completion only once
//         if (!progress.HasCompletedLab(CurrentLab.labName))
//         {
//             progress.MarkLabCompleted(CurrentLab.labName);
//             ProgressManager.Instance.AddStars(starsEarned);
//             ProgressManager.Instance.SaveProgress();

//             Debug.Log($"✅ Lab '{CurrentLab.labName}' completed! +{starsEarned} stars");
//         }

//         CurrentLab = null;

//         // SceneManager.LoadScene("Campus");
//     }
// }

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
//         if (ProgressManager.Instance.HasCompletedLab(lab.labName))
//         {
//             Debug.LogWarning($"Lab '{lab.labName}' already completed in Semester {ProgressManager.Instance.GetCurrentSemester()}!");
//             CurrentLab = null;
//             return;
//         }

//         CurrentLab = lab;
//         Debug.Log($"Starting Lab: {lab.labName} (Semester {ProgressManager.Instance.GetCurrentSemester()})");
//     }

//     public void CompleteLab(int starsEarned)
//     {
//         if (CurrentLab == null) return;

//         if (!ProgressManager.Instance.HasCompletedLab(CurrentLab.labName))
//         {
//             ProgressManager.Instance.CompleteLab(CurrentLab.labName);
//             ProgressManager.Instance.AddStars(starsEarned);
//             Debug.Log($"✅ Lab '{CurrentLab.labName}' completed! +{starsEarned} stars");
//         }

//         CurrentLab = null;
//     }
// }
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
        if (ProgressManager.Instance.HasCompletedLab(lab))
        {
            Debug.LogWarning("You already completed this lab this semester!");
            return;
        }

        CurrentLab = lab;


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
                    // experiment.followUpQuestions = entry.followUpQuestions;
                }
                break;

    }

    }

    public void CompleteLab(int starsEarned)
    {
        if (CurrentLab == null) return;

        ProgressManager.Instance.CompleteLab(CurrentLab, starsEarned);
        CurrentLab = null;
    }
}
