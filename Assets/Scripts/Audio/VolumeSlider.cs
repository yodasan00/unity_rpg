using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
       
        slider = GetComponent<Slider>();
        // 1. Find the MusicManager (even if it's from another scene)
        if (MusicManager.Instance != null)
        {
            // Set the slider visual position to match the actual volume
            slider.value = MusicManager.Instance.masterVolume;

            // 2. Connect the slider to the function via code (Prevents broken links)
            slider.onValueChanged.AddListener(val => MusicManager.Instance.SetMasterVolume(val));
        }
    }
}