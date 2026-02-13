using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Music Library", fileName = "MusicLibrary")]
public class MusicLibrary : ScriptableObject
{
    [Header("Background Music")]
    public AudioClip mainMenuMusic;
    public AudioClip campusMusic;
    public AudioClip BuildingMusic;

    [Header("Lab Music")]
    public AudioClip programmingLabMusic;
    // public AudioClip electricalLabMusic;
    // public AudioClip chemistryLabMusic;
    // public AudioClip drawingLabMusic;

    [Header(" SFX / Event Sounds")]
    [Header("UI Sounds")]
    public AudioClip uiClick;
    public AudioClip uiHover;
    public AudioClip uiSuccess;
    public AudioClip uiError;
    public AudioClip scoreSound;
    public AudioClip[] MaleNPC;
    public AudioClip[] FemaleNPC;
    public AudioClip LibaryBook;
    public AudioClip nextDaySound;
    public AudioClip ball;
    public AudioClip failure;


    [Header("UI / Misc")]
    public AudioClip pauseMenuMusic;
    public AudioClip ComputerMusic;
}
