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
            return;
        }
    }

    public void StartLab(LabData lab)
    {
        CurrentLab = lab;
        Debug.Log("Starting Lab: " + lab.labName);
        SceneManager.LoadScene(lab.labSceneName);
    }

    public void CompleteLab()
    {
        if (CurrentLab == null) return;

        // Mark as completed in PlayerProgress
        var progress = ProgressManager.Instance.Player;
        if (!progress.HasCompletedLab(CurrentLab.labName))
        {
            progress.MarkLabCompleted(CurrentLab.labName);
            progress.AddStars(CurrentLab.starReward);
            ProgressManager.Instance.SaveProgress();

            Debug.Log($"Lab '{CurrentLab.labName}' completed! +{CurrentLab.starReward} stars");
        }

        CurrentLab = null;

        // After completion, return to campus/main menu
        SceneManager.LoadScene("Campus");
    }
}
