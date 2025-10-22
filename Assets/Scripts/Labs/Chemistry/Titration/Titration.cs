using UnityEngine;
using UnityEngine.EventSystems;

public class TitrationExperiment : ChemistryExperiment, IDropHandler
{
    public AcidFlask targetFlask;
    public string correctChemical = "NaOH";

    public void OnDrop(PointerEventData eventData)
    {
        var dropper = eventData.pointerDrag?.GetComponent<Dropper>();
        if (dropper == null) return;

        if (dropper.chemicalName == correctChemical)
        {
            dropper.DropOnFlask(targetFlask);

            if (targetFlask.IsComplete)
            {
                Debug.Log("Titration Complete!");
                CompleteExperiment(3); 
            }
        }
        else
        {
            Debug.Log("⚠️ Wrong chemical!");
        }
    }
}
