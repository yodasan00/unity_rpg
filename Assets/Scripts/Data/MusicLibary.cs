using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Music Library", fileName = "MusicLibrary")]
public class MusicLibrary : ScriptableObject
{
    [Header("Background Music")]
    public AudioClip mainMenuMusic;
    public AudioClip campusMusic;

    [Header("Lab Music")]
    public AudioClip programmingLabMusic;
    // public AudioClip electricalLabMusic;
    // public AudioClip chemistryLabMusic;
    // public AudioClip drawingLabMusic;

    [Header(" SFX / Event Sounds")]
    public AudioClip questCompleteSFX;
    public AudioClip achievementSFX;
    public AudioClip buttonClickSFX;

    [Header("UI / Misc")]
    public AudioClip pauseMenuMusic;
}
