using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [Header("HUD Elements")]
    public TMP_Text starText;
    public TMP_Text semesterText;
    public TMP_Text timeText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateStars(0);
        UpdateSemester("Semester 1");
        UpdateTime("Day 1 - 08:00 AM");
    }

    public void UpdateStars(int stars)
    {
        starText.text = $"Stars: {stars} ";
    }

    public void UpdateSemester(string sem)
    {
        semesterText.text = $"Semester: {sem} ";
    }

    public void UpdateTime(string time)
    {
        timeText.text = $"Time: {time} ";
    }
}
