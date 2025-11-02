
using System.Collections.Generic;
using UnityEngine;

public class SemesterManager : MonoBehaviour
{
    public static SemesterManager Instance { get; private set; }

    [Header("All semesters in order")]
    public List<SemesterData> semesters;

    public SemesterData CurrentSemester { get; private set; }
    public int CurrentSemesterIndex { get; private set; }

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

    private void Start()
    {
        LoadCurrentSemester();
    }

    public void LoadCurrentSemester()
    {
        int currentIndex = ProgressManager.Instance.playerProgress.currentSemester - 1;
        if (currentIndex >= 0 && currentIndex < semesters.Count)
        {
            CurrentSemester = semesters[currentIndex];
            CurrentSemesterIndex = currentIndex;
            Debug.Log("Loaded semester: " + CurrentSemester.semesterName);
            //HUDManager.Instance.UpdateSemester(CurrentSemester.semesterName);
        }
        else
        {
            Debug.LogWarning("Semester index out of range.");
        }
    }

    public bool CanUnlockNextSemester()
    {
        int currentIndex = ProgressManager.Instance.playerProgress.currentSemester - 1;
        if (currentIndex + 1 >= semesters.Count)
            return false;

        SemesterData nextSem = semesters[currentIndex + 1];
        return ProgressManager.Instance.playerProgress.totalStars >= nextSem.requiredStarsToUnlock;
    }

    public void TryUnlockNextSemester()
{
    var progress = ProgressManager.Instance.playerProgress;

    if (CanUnlockNextSemester())
    {
        // 🔹 Move all daily completed labs into permanent completed list
        foreach (var key in progress.dailyCompletedLabs)
        {
            if (!progress.completedLabs.Contains(key))
                progress.completedLabs.Add(key);
        }

        // Clear daily progress and move to next semester
        progress.ResetDailyProgress();
        progress.UnlockNextSemester();
        ProgressManager.Instance.SaveProgress();

        LoadCurrentSemester();

        Debug.Log($"🎓 Next semester unlocked: {CurrentSemester.semesterName}");
     //   HUDManager.Instance.UpdateSemester(CurrentSemester.semesterName);
    }
    else
    {
        Debug.Log("Not enough stars to unlock next semester.");
    }
}

}

//manages semesters including loading current sem and unlocking next sem on star