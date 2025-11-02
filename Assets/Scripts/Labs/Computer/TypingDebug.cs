// using UnityEngine;
// using TMPro;
// using UnityEngine.UI;
// using System.Collections.Generic;

// public class TypingDebug : MonoBehaviour
// {
//     [Header("UI References")]
//     [SerializeField] private TMP_InputField codeInputField;
//     [SerializeField] private TMP_Text consoleOutput;
//     [SerializeField] private TMP_Text scoreText;
//     [SerializeField] private Button runButton;
//     [SerializeField] private Button nextButton;
//     [SerializeField] private GameObject CmdScreen;
//     [SerializeField] private GameObject ScorePanel;
//     [SerializeField] private TMP_Text FinalscoreText;

//     private List<TypingQuestion> questions;
//     private int currentIndex = 0;
//     private int score = 0;
//     public void StartTypingMode(QuestionSet set)
//     {
//         if (set == null || set.typingQuestions == null || set.typingQuestions.Length == 0)
//         {
//             Debug.LogError("QuestionSet is null or has no typing questions!");
//             return;
//         }

//         questions = new List<TypingQuestion>(set.typingQuestions);
//         currentIndex = 0;
//         score = 0;
//         CmdScreen.SetActive(false);
//         ScorePanel.SetActive(false);
//         runButton.interactable = true;
//         nextButton.interactable = true;

//         LoadSnippet();
//         UpdateScore();
//     }

//     private void LoadSnippet()
//     {
//         if (currentIndex < questions.Count)
//         {
//             codeInputField.text = questions[currentIndex].buggyCode;
//             consoleOutput.text = "";
//         }
//         else
//         {
//             CmdScreen.SetActive(true);
//             consoleOutput.text = "All questions done!";
//             runButton.interactable = false;
//             nextButton.interactable = false;
//             ScorePanel.SetActive(true);
//             FinalscoreText.text = $"Score: {score}";

//             // Add stars and save progress
//             LabManager.Instance.CompleteLab(score);
//         }
//     }

//     public void CheckCode()
//     {
//         string playerCode = codeInputField.text.Trim();
//         string correctCode = questions[currentIndex].correctCode.Trim();

//         CmdScreen.SetActive(true);

//         if (playerCode == correctCode)
//         {
//             consoleOutput.text = $"CMD>C:TurboC++/bin/output: {questions[currentIndex].expectedOutput}";
//             score++;
//             UpdateScore();
//         }
//         else
//         {
//             consoleOutput.text = "CMD>C:TurboC++/bin/output: Syntax Error";
//         }
//     }

//     public void LoadNextSnippet()
//     {
//         currentIndex++;
//         CmdScreen.SetActive(false);
//         LoadSnippet();
//     }

//     private void UpdateScore()
//     {
//         scoreText.text = $"Score: {score}";
//     }
// }

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class TypingDebug : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField codeInputField;
    [SerializeField] private TMP_Text consoleOutput;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Button runButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private GameObject CmdScreen;
    [SerializeField] private GameObject ScorePanel;
    [SerializeField] private TMP_Text FinalscoreText;

    private List<TypingQuestion> questions;
    private int currentIndex = 0;
    private int score = 0;

    public void StartTypingMode(QuestionSet set)
    {
        if (set == null || set.typingQuestions == null || set.typingQuestions.Length == 0)
        {
            Debug.LogError("QuestionSet is null or has no typing questions!");
            return;
        }

        // Step 1: Copy all typing questions
        questions = new List<TypingQuestion>(set.typingQuestions);

        // Step 2: Shuffle all typing questions
        ShuffleQuestions();

        // Step 3: Take only 2 questions (or fewer if there are less than 2)
        int questionCount = Mathf.Min(2, questions.Count);
        questions = questions.GetRange(0, questionCount);

        currentIndex = 0;
        score = 0;
        CmdScreen.SetActive(false);
        ScorePanel.SetActive(false);
        runButton.interactable = true;
        nextButton.interactable = true;

        LoadSnippet();
        UpdateScore();
    }

    private void LoadSnippet()
    {
        if (currentIndex < questions.Count)
        {
            codeInputField.text = questions[currentIndex].buggyCode;
            consoleOutput.text = "";
        }
        else
        {
            CmdScreen.SetActive(true);
            consoleOutput.text = "All questions done!";
            runButton.interactable = false;
            nextButton.interactable = false;
            ScorePanel.SetActive(true);
            FinalscoreText.text = $"Score: {score}";

            // Add stars and save progress
            if (LabManager.Instance != null)
                LabManager.Instance.CompleteLab(score);
            else
                Debug.LogError("LabManager instance not found!");
        }
    }

    public void CheckCode()
    {
        string playerCode = codeInputField.text.Trim();
        string correctCode = questions[currentIndex].correctCode.Trim();

        CmdScreen.SetActive(true);

        if (playerCode == correctCode)
        {
            consoleOutput.text = $"CMD>C:TurboC++/bin/output: {questions[currentIndex].expectedOutput}";
            score++;
            UpdateScore();
        }
        else
        {
            consoleOutput.text = "CMD>C:TurboC++/bin/output: Syntax Error";
        }
    }

    public void LoadNextSnippet()
    {
        currentIndex++;
        CmdScreen.SetActive(false);
        LoadSnippet();
    }

    private void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }

    private void ShuffleQuestions()
    {
        for (int i = 0; i < questions.Count; i++)
        {
            int randomIndex = Random.Range(i, questions.Count);
            var temp = questions[i];
            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp;
        }
    }
}
