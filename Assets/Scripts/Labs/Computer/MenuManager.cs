

using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("UI Screens")]
    [SerializeField] private GameObject MenuScreen;
    [SerializeField] private GameObject MCQCanvas;
    [SerializeField] private GameObject TypingCanvas;

    [Header("Managers")]
    [SerializeField] private GameObject McqManager;
    [SerializeField] private GameObject TypingManager;

    private void Start()
    {
        MenuScreen.SetActive(true);
        MCQCanvas.SetActive(false);
        TypingCanvas.SetActive(false);
    }

    public void CloseLabScreen()
    {
        MCQCanvas.SetActive(false);
        TypingCanvas.SetActive(false);
        MenuScreen.SetActive(true);
    }

    public void OpenMCQ() //foe button press
    {
        LabData lab = GetNextLab();
        if (lab != null)
            OpenLab(lab, LabMode.MCQ);
    }

    public void OpenTyping() //same
    {
        LabData lab = GetNextLab();
        if (lab != null)
            OpenLab(lab, LabMode.Typing);
    }

    private LabData GetNextLab()
    {
        var labs = SemesterManager.Instance.CurrentSemester.labs;

        foreach (var lab in labs)
        {
            if (!ProgressManager.Instance.HasCompletedLab(lab))
                return lab;
        }

        Debug.LogWarning("All labs completed in this semester!");
        return null;
    }

    private void OpenLab(LabData lab, LabMode mode)
    {
        LabManager.Instance.StartLab(lab);
        MenuScreen.SetActive(false);

        switch (mode)
        {
            case LabMode.MCQ:
                MCQCanvas.SetActive(true);
                var mcq = McqManager.GetComponent<ComputerMCQ>();
                if (mcq != null)
                {
                    var set = QuestionManager.Instance.GetQuestionsForLab(lab.labName, lab.subjectName); //get question set from the so
                    if (set != null)
                        mcq.StartMCQ(set);
                }
                break;

            case LabMode.Typing:
                TypingCanvas.SetActive(true);
                var typing = TypingManager.GetComponent<TypingDebug>();
                if (typing != null)
                {
                    var set = QuestionManager.Instance.GetQuestionsForLab(lab.labName, lab.subjectName); //grt question set from the so
                    if (set != null)
                        typing.StartTypingMode(set);
                }
                break;
        }
    }
}
