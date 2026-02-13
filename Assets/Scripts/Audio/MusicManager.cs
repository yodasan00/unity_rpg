

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Music Configuration")]
    public MusicLibrary musicLibrary;

    [Header("Master Settings")]
    [Range(0f, 1f)] public float masterVolume = 1f;

    [Header("State (Read Only)")]
    public bool musicEnabled = true;
    public bool sfxEnabled = true;

    [Range(0.1f, 5f)] public float fadeDuration = 1.5f;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private Coroutine fadeCoroutine;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Setup Audio Sources
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        sfxSource.loop = false;

        // Apply Initial Volume
        musicSource.volume = musicEnabled ? masterVolume : 0f;
        sfxSource.volume = sfxEnabled ? masterVolume : 0f;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (musicLibrary == null) return;

        string sceneName = scene.name.ToLower();
        AudioClip nextClip = null;

        if (sceneName.Contains("mainmenu"))
            nextClip = musicLibrary.mainMenuMusic;
        else if (sceneName.Contains("main"))
            nextClip = musicLibrary.campusMusic;
        else if (sceneName.Contains("programming"))
            nextClip = musicLibrary.programmingLabMusic;
        else if (sceneName.Contains("pause"))
            nextClip = musicLibrary.pauseMenuMusic;
        else
            nextClip = musicLibrary.BuildingMusic;

        if (nextClip != null)
            PlayMusic(nextClip);
    }

    // 🎵 MUSIC PLAYER (Handles Song Swapping & Fading)
    public void PlayMusic(AudioClip clip)
    {
        if (!musicEnabled) return;
        if (clip == null) return;

        // If same song is already playing, just ensure volume is correct
        if (musicSource.clip == clip && musicSource.isPlaying)
        {
            musicSource.volume = masterVolume;
            return;
        }

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeToNewClip(clip));
    }

    // 🏆 SFX PLAYER
    public void PlaySFX(AudioClip clip)
    {
        if (!sfxEnabled) return;
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, masterVolume);
    }

    // 🌫 FADE COROUTINE
    private IEnumerator FadeToNewClip(AudioClip newClip)
    {
        // Fade Out
        if (musicSource.isPlaying)
        {
            float startVol = musicSource.volume;
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(startVol, 0, t / fadeDuration);
                yield return null;
            }
        }

        musicSource.Stop();
        musicSource.clip = newClip;

        // Start New Song
        if (musicEnabled)
        {
            musicSource.Play();
            // Fade In
            for (float t = 0; t < fadeDuration; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(0, masterVolume, t / fadeDuration);
                yield return null;
            }
            // Ensure final volume is set
            musicSource.volume = masterVolume;
        }
        fadeCoroutine = null;
    }

    // ==========================================
    // 🎚️ UI CONTROLS
    // ==========================================

    // 1. MASTER SLIDER
    public void SetMasterVolume(float value)
    {
        masterVolume = value;

        // Immediate update for SFX
        if (sfxEnabled) sfxSource.volume = masterVolume;

        // Immediate update for Music (Overrides any running fade to prevent sticking)
        if (musicEnabled) musicSource.volume = masterVolume;
    }

    // 2. MUSIC TOGGLE BUTTON
    public void ToggleMusic()
    {
        musicEnabled = !musicEnabled;

        if (musicEnabled)
        {
            // ✅ THE FIX: Force volume immediately
            musicSource.volume = masterVolume;

            // If we have a song loaded but stopped, Play it.
            if (musicSource.clip != null && !musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
        else
        {
            // Mute and Stop
            musicSource.volume = 0;
            musicSource.Stop();
        }
    }

    // 3. SFX TOGGLE BUTTON
    public void ToggleSFX()
    {
        sfxEnabled = !sfxEnabled;
        // Optional: Update source volume immediately so next PlayOneShot is correct
        sfxSource.volume = sfxEnabled ? masterVolume : 0f;
    }

    // ==========================================
    // UI SOUNDS
    // ==========================================
    public void PlayUISound(string type)
    {
        if (musicLibrary == null) return;

        switch (type.ToLower())
        {
            case "click": PlaySFX(musicLibrary.uiClick); break;
            case "hover": PlaySFX(musicLibrary.uiHover); break;
            case "success": PlaySFX(musicLibrary.uiSuccess); break;
            case "error": PlaySFX(musicLibrary.uiError); break;
            case "score": PlaySFX(musicLibrary.scoreSound); break;
            case "book": PlaySFX(musicLibrary.LibaryBook); break;
            case "male":
                if (musicLibrary.MaleNPC != null && musicLibrary.MaleNPC.Length > 0)
                    PlaySFX(musicLibrary.MaleNPC[Random.Range(0, musicLibrary.MaleNPC.Length)]);
                break;
            case "female":
                if (musicLibrary.FemaleNPC != null && musicLibrary.FemaleNPC.Length > 0)
                    PlaySFX(musicLibrary.FemaleNPC[Random.Range(0, musicLibrary.FemaleNPC.Length)]);
                break;
            case "nextday": PlaySFX(musicLibrary.nextDaySound); break;
            case "ball": PlaySFX(musicLibrary.ball); break;
            case "failure": PlaySFX(musicLibrary.failure); break;
        }
    }
    
    public void StopMusic() => musicSource.Stop();
}