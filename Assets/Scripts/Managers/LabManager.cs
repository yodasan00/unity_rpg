
using UnityEngine;

public class LabManager : MonoBehaviour
{
    public static LabManager Instance { get; private set; }
    public LabData CurrentLab { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLab(LabData lab)
    {
        if (ProgressManager.Instance.HasCompletedLab(lab))
        {
            Debug.LogWarning("You already completed this lab this semester!");
            return;
        }

        CurrentLab = lab;


        switch (lab.labType) // for adding labs later since my early version has menumanager only for computer lab i have added only the chem lab here and later will add everthing in this logic
        {
       
        case LabType.Chemistry:
                if (lab.chemistryExperiments != null && lab.chemistryExperiments.Count > 0)
                {
                    int index = Random.Range(0, lab.chemistryExperiments.Count);
                    var entry = lab.chemistryExperiments[index];

                    GameObject instance = Instantiate(entry.experimentPrefab);
                    ChemistryExperiment experiment = instance.GetComponent<ChemistryExperiment>();
                    experiment.labData = lab;
                    experiment.experimentName = entry.experimentName;
                    
                }
                break;

    }

    }

    public void CompleteLab(int starsEarned)
    {
        if (CurrentLab == null) return;

        ProgressManager.Instance.CompleteLab(CurrentLab, starsEarned);
        CurrentLab = null;
    }
}


//used to manage the labs and their states