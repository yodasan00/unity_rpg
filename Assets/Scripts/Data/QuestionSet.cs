using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestionSet", menuName = "Labs/QuestionSet")]
public class QuestionSet : ScriptableObject
{
    [Header("MCQ Questions")]
    public QuestionData[] questions;          //MCq haru

    [Header("Typing / Code Questions")]
    public TypingQuestion[] typingQuestions;  //Coding 
}

[System.Serializable]
public class QuestionData
{
    public string questionText;   //Question text
    public string[] options;      //Answer options
    public string correctOption;     //correct answer
}

[System.Serializable]
public class TypingQuestion
{
    public string buggyCode;       //buggy code
    public string correctCode;     //Correct code
    public string expectedOutput;  //Expected output 
}
