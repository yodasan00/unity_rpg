using UnityEngine;
using UnityEngine.UI;

// Removed RequireComponent(typeof(Button)) as we only modify the Image now
[RequireComponent(typeof(Image))]
public class AudioButton : MonoBehaviour
{
    [Header("Button Type")]
    [Tooltip("Check this if this button controls Music. Uncheck if it controls SFX.")]
    public bool isMusicButton = true; 

    [Header("Icons")]
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private Image targetImage;

    void Start()
    {
        targetImage = GetComponent<Image>();
        UpdateIcon();
    }

    void Update()
    {
        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (MusicManager.Instance == null || targetImage == null) return;

        // Check which boolean to look at
        bool isOn = isMusicButton ? MusicManager.Instance.musicEnabled : MusicManager.Instance.sfxEnabled;
        
        // Determine correct sprite
        Sprite requiredSprite = isOn ? soundOnSprite : soundOffSprite;

        // Apply only if changed (Optimized to prevent Unity UI redraws)
        if (targetImage.sprite != requiredSprite)
        {
            targetImage.sprite = requiredSprite;
        }
    }
}