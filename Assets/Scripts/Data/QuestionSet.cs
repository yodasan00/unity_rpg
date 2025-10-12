using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionSet", menuName = "Game/Question Set")]
public class QuestionSet : ScriptableObject
{
    public string setName;
    public List<QuestionData> questions = new List<QuestionData>();
}
