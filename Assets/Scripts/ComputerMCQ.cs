using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class ComputerMCQ : MonoBehaviour
{
    [SerializeField]
    private TMP_Text questionText;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private TMP_Text remainingText;
    [SerializeField]
    private Button[] answerButtons;
    [SerializeField]
    private TMP_Text ScoreText;
    [SerializeField]
    private GameObject ScorePanel;

    [SerializeField]
    private int NoOfQuestions = 3;

    private int score = 0;
    private int currentQuestion = 0;

    [SerializeField]
    private string[] questions = {
        "Which language is compiled?",
        "Which symbol is used to end a statement in C?",
        "What does HTML stand for?"
    };
    [SerializeField]
    private string[][] options = {
        new string[] {"Python", "Java", "HTML", "CSS"},
        new string[] {".", ";", ":", ","},
        new string[] {"Hyper Trainer Markup Language", "Hyper Text Markup Language", "Home Tool Markup Language", "Hyperlinks and Text Markup Language"}
    };
    [SerializeField]
    private int[] correctAnswers = { 1, 1, 1 }; // indexes of correct answers

    void Start()
    {
        ShuffleQuestions();
        LoadQuestion();
        ScorePanel.SetActive(false);
    }

    public void Initialize()
    {
        score = 0;
        currentQuestion = 0;
        ShuffleQuestions();
        LoadQuestion();
        
        ScorePanel.SetActive(false);
    }

    void LoadQuestion()
    {
        questionText.text = questions[currentQuestion];
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TMP_Text>().text = options[currentQuestion][i];
            int index = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
        remainingText.text = $"Remaining: {NoOfQuestions - currentQuestion}/{NoOfQuestions}";
        scoreText.text = $"Score: {score}";
    }

    void CheckAnswer(int choice)
    {
        if (choice == correctAnswers[currentQuestion])
        {
            score++;
        }
        currentQuestion++;
        if (currentQuestion < questions.Length)
            LoadQuestion();
        else
        {
            ScorePanel.SetActive(true);
            ScoreText.text = $"Final Score: {score} Stars";
            questionText.text = "Game Over!";
        }
    }

    void ShuffleQuestions()
    {
        Debug.Log("Shuffling Questions");
        for (int i = 0; i < NoOfQuestions; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, NoOfQuestions);
            string temp1 = questions[i];
            questions[i] = questions[randomIndex];
            questions[randomIndex] = temp1;

            String[] temp2 = options[i];
            options[i] = options[randomIndex];
            options[randomIndex] = temp2;

            int temp3 = correctAnswers[i];
            correctAnswers[i] = correctAnswers[randomIndex];
            correctAnswers[randomIndex] = temp3;
        }
    }

    
    
}
