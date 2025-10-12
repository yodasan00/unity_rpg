using System;
using UnityEngine;

[Serializable]
public class QuestionData
{
    [Header("Multiple Choice Question")]
    public string questionText;
    public string[] options;     // For multiple-choice questions
    public int correctOption;    // index of correct answer
    public string explanation;   // optional feedback
}
