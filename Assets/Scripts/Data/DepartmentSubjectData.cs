using UnityEngine;
using System;
using System.Collections.Generic;

// Note: This nested class does NOT need to be a ScriptableObject; it's just data.
[Serializable]
public class Subject
{
    public string subjectName;
    [Tooltip("The URL to be loaded by the Unity Web Browser.")]
    public string url;
}

[CreateAssetMenu(fileName = "New Department Data", menuName = "UI/Department Subjects")]
public class DepartmentSubjectData : ScriptableObject
{
    [Tooltip("The name of the department (e.g., Civil, Computer).")]
    public string departmentName;

    [Tooltip("The list of subjects belonging to this department.")]
    public List<Subject> subjects = new List<Subject>();
}