using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestionSet", menuName = "Labs/QuestionSet")]
public class QuestionSet : ScriptableObject
{
    [Header("MCQ Questions")]
    public QuestionData[] questions;          // Multiple-choice questions

    [Header("Typing / Code Questions")]
    public TypingQuestion[] typingQuestions;  // Coding / typing exercises
}

[System.Serializable]
public class QuestionData
{
    public string questionText;   // Question text
    public string[] options;      // Answer options
    public string correctOption;     // Index of the correct answer
}

[System.Serializable]
public class TypingQuestion
{
    public string buggyCode;       // Code with bugs for the player
    public string correctCode;     // Correct code
    public string expectedOutput;  // Expected output when code is correct
}
