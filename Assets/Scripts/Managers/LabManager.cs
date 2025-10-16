using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Called when player clicks a lab button
    public void StartLab(LabData lab)
    {
        CurrentLab = lab;
       // SceneManager.LoadScene(lab.labSceneName);
    }

    // Called from lab script when lab is complete
    public void CompleteLab(int starsEarned)
    {
        if (CurrentLab == null) return;

        var progress = ProgressManager.Instance.playerProgress;

        // Mark lab completed
        if (!progress.HasCompletedLab(CurrentLab.labName))
        {
            progress.MarkLabCompleted(CurrentLab.labName);
            progress.AddStars(starsEarned); // Add earned stars
            ProgressManager.Instance.SaveProgress();

            Debug.Log($"Lab '{CurrentLab.labName}' completed! +{starsEarned} stars");
        }

        CurrentLab = null;

        // Return to campus/main menu
       // SceneManager.LoadScene("Campus");
    }
}
