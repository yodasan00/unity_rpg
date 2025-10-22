using UnityEngine;
using System.Collections.Generic;

public class ChemistryLabManager : MonoBehaviour
{
    public static ChemistryLabManager Instance { get; private set; }

    [Header("Lab Setup")]
    public LabData chemistryLabData;  // Assign your Chemistry LabData asset
    public Transform experimentParent; // Where to spawn the experiment prefab

    [Header("Available Experiments")]
    public List<ExperimentEntry> experiments; // List of all possible chemistry experiments

    private ChemistryExperiment currentExperiment;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void StartExperiment(string experimentName)
    {
        if (currentExperiment != null)
            Destroy(currentExperiment.gameObject);

        ExperimentEntry selected = experiments.Find(e => e.experimentName == experimentName);

        if (selected == null)
        {
            Debug.LogWarning($"No experiment found with name: {experimentName}");
            return;
        }

        GameObject obj = Instantiate(selected.experimentPrefab, experimentParent);
        currentExperiment = obj.GetComponent<ChemistryExperiment>();

        if (currentExperiment != null)
        {
            currentExperiment.labData = chemistryLabData; // Assign Chemistry Lab
            currentExperiment.experimentName = selected.experimentName;

            Debug.Log($"🧪 Started experiment: {selected.experimentName}");
        }
        else
        {
            Debug.LogError($"❌ Prefab {selected.experimentPrefab.name} does not have ChemistryExperiment component!");
        }
    }

    public void EndExperiment()
    {
        if (currentExperiment != null)
        {
            Destroy(currentExperiment.gameObject);
            currentExperiment = null;
        }
    }
}

