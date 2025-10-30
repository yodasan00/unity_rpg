// using UnityEngine;
// using TMPro;
// using UnityEngine.UI;
// using System.Collections.Generic;
// using System;

// public class ComputerMCQ : MonoBehaviour
// {
//     [SerializeField]
//     private TMP_Text questionText;
//     [SerializeField]
//     private TMP_Text scoreText;
//     [SerializeField]
//     private TMP_Text remainingText;
//     [SerializeField]
//     private Button[] answerButtons;
//     [SerializeField]
//     private TMP_Text ScoreText;
//     [SerializeField]
//     private GameObject ScorePanel;

//     [SerializeField]
//     private int NoOfQuestions = 3;

//     private int score = 0;
//     private int currentQuestion = 0;

//     [SerializeField]
//     private string[] questions = {
//         "Which language is compiled?",
//         "Which symbol is used to end a statement in C?",
//         "What does HTML stand for?"
//     };
//     [SerializeField]
//     private string[][] options = {
//         new string[] {"Python", "Java", "HTML", "CSS"},
//         new string[] {".", ";", ":", ","},
//         new string[] {"Hyper Trainer Markup Language", "Hyper Text Markup Language", "Home Tool Markup Language", "Hyperlinks and Text Markup Language"}
//     };
//     [SerializeField]
//     private int[] correctAnswers = { 1, 1, 1 }; // indexes of correct answers

//     void Start()
//     {
//         ShuffleQuestions();
//         LoadQuestion();
//         ScorePanel.SetActive(false);
//     }

//     public void Initialize()
//     {
//         score = 0;
//         currentQuestion = 0;
//         ShuffleQuestions();
//         LoadQuestion();
        
//         ScorePanel.SetActive(false);
//     }

//     void LoadQuestion()
//     {
//         questionText.text = questions[currentQuestion];
//         for (int i = 0; i < answerButtons.Length; i++)
//         {
//             answerButtons[i].GetComponentInChildren<TMP_Text>().text = options[currentQuestion][i];
//             int index = i;
//             answerButtons[i].onClick.RemoveAllListeners();
//             answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
//         }
//         remainingText.text = $"Remaining: {NoOfQuestions - currentQuestion}/{NoOfQuestions}";
//         scoreText.text = $"Score: {score}";
//     }

//     void CheckAnswer(int choice)
//     {
//         if (choice == correctAnswers[currentQuestion])
//         {
//             score++;
//         }
//         currentQuestion++;
//         if (currentQuestion < questions.Length)
//             LoadQuestion();
//         else
//         {
//             ScorePanel.SetActive(true);
//             ScoreText.text = $"Final Score: {score} Stars";
//             questionText.text = "Game Over!";
//         }
//     }

//     void ShuffleQuestions()
//     {
//         Debug.Log("Shuffling Questions");
//         for (int i = 0; i < NoOfQuestions; i++)
//         {
//             int randomIndex = UnityEngine.Random.Range(i, NoOfQuestions);
//             string temp1 = questions[i];
//             questions[i] = questions[randomIndex];
//             questions[randomIndex] = temp1;

//             String[] temp2 = options[i];
//             options[i] = options[randomIndex];
//             options[randomIndex] = temp2;

//             int temp3 = correctAnswers[i];
//             correctAnswers[i] = correctAnswers[randomIndex];
//             correctAnswers[randomIndex] = temp3;
//         }
//     }

    
    
// }

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ComputerMCQ : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text questionText;
    public TMP_Text remainingText;
    public TMP_Text scoreText;
    public Button[] answerButtons;
    public GameObject resultPanel;
    public TMP_Text resultText;

    private List<QuestionData> questions;
    private int currentQuestionIndex = 0;
    private int score = 0;

    public void StartMCQ(QuestionSet set)
    {
        if (set == null || set.questions.Length == 0)
        {
            Debug.LogError("QuestionSet is null or empty!");
            return;
        }

        questions = new List<QuestionData>(set.questions); //gets the question set from the menu manger
        ShuffleQuestions(); 
        currentQuestionIndex = 0;
        score = 0;
        resultPanel.SetActive(false);

        ShowQuestion();
    }

    private void ShowQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            EndMCQ();
            return;
        }

        QuestionData q = questions[currentQuestionIndex];
        questionText.text = q.questionText;
        remainingText.text = $"Remaining: {questions.Count - currentQuestionIndex}/{questions.Count}";
        scoreText.text = $"Score: {score}";

       //shuffle options based on the current question and the correct answer
        List<string> shuffledOptions = new List<string>(q.options);
        for (int i = 0; i < shuffledOptions.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffledOptions.Count);
            string temp = shuffledOptions[i];
            shuffledOptions[i] = shuffledOptions[randomIndex];
            shuffledOptions[randomIndex] = temp;
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < shuffledOptions.Count)
            {
                answerButtons[i].gameObject.SetActive(true);
                string optionText = shuffledOptions[i];
                answerButtons[i].GetComponentInChildren<TMP_Text>().text = optionText;

                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(optionText, q.correctOption));
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnAnswerSelected(string selectedAnswer, string correctAnswer)
    {
        if (selectedAnswer == correctAnswer)
            score++;

        currentQuestionIndex++;
        ShowQuestion();
    }

    private void EndMCQ()
    {
        resultPanel.SetActive(true);
        resultText.text = $"Stars Earned: {score}";

        if (resultPanel.activeSelf)
             Debug.Log("Result panel still active!");


        if (LabManager.Instance != null)
        {
            LabManager.Instance.CompleteLab(score); //Notify LabManager with score
        }
        else
        {
            Debug.LogError("LabManager instance not found!");
        }
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
