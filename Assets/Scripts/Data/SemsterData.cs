
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSemester", menuName = "Lab System/SemesterData")]
public class SemesterData : ScriptableObject
{
    public string semesterName;                //e.g. "Semester 1"
    public List<LabData> labs;                 //No. of labs Per sem ma
    public int requiredStarsToUnlock;          //Stars required to unlock nxt sem
}
