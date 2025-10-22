// using UnityEngine;

// public class MenuManager : MonoBehaviour
// {
//     [Header("UI Screens")]
//     [SerializeField] private GameObject ComputerScreen;
//     [SerializeField] private GameObject MenuScreen;
//     [SerializeField] private GameObject MCQCanvas;
//     [SerializeField] private GameObject TypingCanvas;

//     [Header("Managers")]
//     [SerializeField] private GameObject McqManager;
//     [SerializeField] private GameObject TypingManager;

//     public PlayerController Player;

//     private void Start()
//     {
//         Debug.Log("Menu Manager started");
//         MenuScreen.SetActive(true);
//         MCQCanvas.SetActive(false);
//         TypingCanvas.SetActive(false);
//     }

//     public void CloseComputerScreen()
//     {
//         ComputerScreen.SetActive(false);
//         MCQCanvas.SetActive(false);
//         TypingCanvas.SetActive(false);
//         MenuScreen.SetActive(true);
//         Player.enabled = true;
//     }

//     public void OpenMCQ()
//     {
//         Debug.Log("MCQ Button Clicked");

//         MenuScreen.SetActive(false);
//         MCQCanvas.SetActive(true);

//         var mcq = McqManager.GetComponent<ComputerMCQ>();
//         if (mcq != null)
//         {
//             var lab = LabManager.Instance.CurrentLab;
//             if (lab != null)
//             {
//                 var set = QuestionManager.Instance.GetQuestionsForLab(lab.labName);
//                 if (set != null)
//                 {
//                     mcq.StartMCQ(set);
//                 }
//                 else
//                 {
//                     Debug.LogError("QuestionSet not found for this lab!");
//                 }
//             }
//             else
//             {
//                 Debug.LogError("No active lab in LabManager!");
//             }
//         }
//         else
//         {
//             Debug.LogError("ComputerMCQ script missing on McqManager!");
//         }
//     }

//     public void OpenTyping()
//     {
//         Debug.Log("Typing Button Clicked");
//         MenuScreen.SetActive(false);
//         TypingCanvas.SetActive(true);

//         var typing = TypingManager.GetComponent<TypingDebug>();
//         if (typing != null)
//         {
//             var lab = LabManager.Instance.CurrentLab;
//             if (lab != null)
//             {
//                 var set = QuestionManager.Instance.GetQuestionsForLab(lab.labName);
//                 if (set != null)
//                 {
//                     typing.StartTypingMode(set);
//                 }
//                 else
//                 {
//                     Debug.LogError("QuestionSet not found for this lab!");
//                 }
//             }
//             else
//             {
//                 Debug.LogError("No active lab in LabManager!");
//             }
//         }
//         else
//         {
//             Debug.LogError("TypingDebug script missing on TypingManager!");
//         }
//     }
// }

// using UnityEngine;

// public class MenuManager : MonoBehaviour
// {
//     [SerializeField] private GameObject ComputerScreen;
//     [Header("UI Screens")]
//     [SerializeField] private GameObject MenuScreen;
//     [SerializeField] private GameObject MCQCanvas;
//     [SerializeField] private GameObject TypingCanvas;

//     [Header("Managers")]
//     [SerializeField] private GameObject McqManager;
//     [SerializeField] private GameObject TypingManager;

//     public void OpenProgrammingMCQ() => OpenLab(programmingLabData, LabMode.MCQ);
//     public void OpenProgrammingTyping() => OpenLab(programmingLabData, LabMode.Typing);


// // Add fields for lab assets
//     [SerializeField] private LabData programmingLabData;


//     public PlayerController Player;

//     private void Start()
//     {
//         MenuScreen.SetActive(true);
//         MCQCanvas.SetActive(false);
//         TypingCanvas.SetActive(false);
//     }

//     public void CloseLabScreen()
//     {
//         MCQCanvas.SetActive(false);
//         TypingCanvas.SetActive(false);
//         MenuScreen.SetActive(false);
//         ComputerScreen.SetActive(false);
//         Player.enabled = true;
//     }

//     /// <summary>
//     /// Generic function to open any lab in any mode
//     /// </summary>
//     public void OpenLab(LabData lab, LabMode mode)
//     {
//         LabManager.Instance.StartLab(lab); // Set current lab

//         MenuScreen.SetActive(false);

//         Debug. Log($"Opening {lab.labName} in {mode} mode");

//         switch (mode)
//         {
//             case LabMode.MCQ:
//                 MCQCanvas.SetActive(true);
//                 var mcq = McqManager.GetComponent<ComputerMCQ>();
//                 if (mcq != null)
//                 {
//                     var set = QuestionManager.Instance.GetQuestionsForLab(lab.labName);
//                     if (set != null)
//                         mcq.StartMCQ(set);
//                     else
//                         Debug.LogError("QuestionSet not found for this lab!");
//                 }
//                 break;

//             case LabMode.Typing:
//                 TypingCanvas.SetActive(true);
//                 var typing = TypingManager.GetComponent<TypingDebug>();
//                 if (typing != null)
//                 {
//                     var set = QuestionManager.Instance.GetQuestionsForLab(lab.labName);
//                     if (set != null)
//                         typing.StartTypingMode(set);
//                     else
//                         Debug.LogError("QuestionSet not found for this lab!");
//                 }
//                 break;
//         }
//     }
// }

// using UnityEngine;

// public class MenuManager : MonoBehaviour
// {
//     [SerializeField] private GameObject ComputerScreen;

//     [Header("UI Screens")]
//     [SerializeField] private GameObject MenuScreen;
//     [SerializeField] private GameObject MCQCanvas;
//     [SerializeField] private GameObject TypingCanvas;

//     [Header("Managers")]
//     [SerializeField] private GameObject McqManager;
//     [SerializeField] private GameObject TypingManager;

//     public PlayerController Player;

//     private void Start()
//     {
//         MenuScreen.SetActive(true);
//         MCQCanvas.SetActive(false);
//         TypingCanvas.SetActive(false);
//     }

//     public void CloseLabScreen()
//     {
//         MCQCanvas.SetActive(false);
//         TypingCanvas.SetActive(false);
//         MenuScreen.SetActive(false);
//         ComputerScreen.SetActive(false);
//         Player.enabled = true;
//     }

//     // Opens a programming MCQ lab for the current semester
//     public void OpenProgrammingMCQ()
//     {
//         LabData lab = GetCurrentSemesterLab("ProgrammingLab");
//         if (lab != null) OpenLab(lab, LabMode.MCQ);
//         else Debug.LogWarning("No Programming Lab found for this semester!");
//     }

//     // Opens a programming Typing lab for the current semester
//     public void OpenProgrammingTyping()
//     {
//         LabData lab = GetCurrentSemesterLab("ProgrammingLab");
//         if (lab != null) OpenLab(lab, LabMode.Typing);
//         else Debug.LogWarning("No Programming Lab found for this semester!");
//     }

//     private LabData GetCurrentSemesterLab(string labName)
//     {
//         var currentSemester = SemesterManager.Instance.CurrentSemester;
//         if (currentSemester == null)
//         {
//             Debug.LogError("Current semester not loaded!");
//             return null;
//         }

//         foreach (var lab in currentSemester.labs)
//         {
//             if (lab.labName == labName) return lab;
//         }
//         return null;
//     }

//     private void OpenLab(LabData lab, LabMode mode)
//     {
//         LabManager.Instance.StartLab(lab);
//         if (LabManager.Instance.CurrentLab == null)
//         {
//             Debug.LogWarning("Cannot open lab, already completed this semester");
//             return;
//         }

//         MenuScreen.SetActive(false);
//         Debug.Log($"Opening {lab.labName} in {mode} mode");

//         switch (mode)
//         {
//             case LabMode.MCQ:
//                 MCQCanvas.SetActive(true);
//                 var mcq = McqManager.GetComponent<ComputerMCQ>();
//                 if (mcq != null)
//                 {
//                     var set = QuestionManager.Instance.GetQuestionsForLab(lab.labName);
//                     if (set != null) mcq.StartMCQ(set);
//                     else Debug.LogError("QuestionSet not found for this lab!");
//                 }
//                 break;

//             case LabMode.Typing:
//                 TypingCanvas.SetActive(true);
//                 var typing = TypingManager.GetComponent<TypingDebug>();
//                 if (typing != null)
//                 {
//                     var set = QuestionManager.Instance.GetQuestionsForLab(lab.labName);
//                     if (set != null) typing.StartTypingMode(set);
//                     else Debug.LogError("QuestionSet not found for this lab!");
//                 }
//                 break;
//         }
//     }
// }

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

    public void OpenMCQ()
    {
        LabData lab = GetNextLab();
        if (lab != null)
            OpenLab(lab, LabMode.MCQ);
    }

    public void OpenTyping()
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
                    var set = QuestionManager.Instance.GetQuestionsForLab(lab.labName, lab.subjectName);
                    if (set != null)
                        mcq.StartMCQ(set);
                }
                break;

            case LabMode.Typing:
                TypingCanvas.SetActive(true);
                var typing = TypingManager.GetComponent<TypingDebug>();
                if (typing != null)
                {
                    var set = QuestionManager.Instance.GetQuestionsForLab(lab.labName, lab.subjectName);
                    if (set != null)
                        typing.StartTypingMode(set);
                }
                break;
        }
    }
}
