
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
        if (CanUnlockNextSemester())
        {
            ProgressManager.Instance.playerProgress.currentSemester++;
            ProgressManager.Instance.SaveProgress();
            LoadCurrentSemester();
            Debug.Log($"🎓 Next semester unlocked: {CurrentSemester.semesterName}");
        }
        else
        {
            Debug.Log("Not enough stars to unlock next semester.");
        }
    }
}

//manages semesters including loading current sem and unlocking next sem on star