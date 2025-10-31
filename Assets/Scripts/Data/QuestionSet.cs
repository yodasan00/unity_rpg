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
    [TextArea(10, 20)]
    public string buggyCode;
    [TextArea(10, 20)] //buggy code
    public string correctCode;   
    [TextArea(10, 20)]  //Correct code
    public string expectedOutput;  //Expected output 
}
