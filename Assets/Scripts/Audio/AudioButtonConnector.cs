using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AudioButtonConnector : MonoBehaviour
{
    public bool isMusicButton = true; // Check for Music, Uncheck for SFX

    void Start()
    {
        Button btn = GetComponent<Button>();

        // Remove old listeners to prevent double-clicking issues
        btn.onClick.RemoveAllListeners();

        // Add the new listener automatically
        btn.onClick.AddListener(() => 
        {
            if (MusicManager.Instance != null)
            {
                if (isMusicButton)
                    MusicManager.Instance.ToggleMusic();
                else
                    MusicManager.Instance.ToggleSFX();
            }
        });
    }
}