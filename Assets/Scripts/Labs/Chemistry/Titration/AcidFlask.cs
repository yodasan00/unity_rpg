

using UnityEngine;
using UnityEngine.UI;

public class AcidFlask : MonoBehaviour
{
    [SerializeField] private float targetVolume = 10f; // mL required for neutralization
    [SerializeField] private float currentVolume = 0f;

    [SerializeField] private Image indicator;
    
    private Color startColor; 
    private Color endColor;   
    public bool IsComplete => currentVolume >= targetVolume;

    private void Start()
    {
        if (indicator == null)
        {
            Debug.LogError("Indicator Image is not assigned on " + gameObject.name);
            return;
        }
        startColor = Color.white;
        endColor = Color.white;
        startColor.a = 0f; 
        endColor.a = 1f;   
        indicator.color = startColor;
    }

    public void AddBase(float volume)
    {
        currentVolume += volume;

        
        if (indicator != null)
        { 
            float t = Mathf.Clamp01(currentVolume / targetVolume);
            indicator.color = Color.Lerp(startColor, endColor, t);
        }

        if (IsComplete)
            Debug.Log("✅ Reaction complete! Neutralization achieved.");
    }
}