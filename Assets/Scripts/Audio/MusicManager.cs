using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Music Configuration")]
    public MusicLibrary musicLibrary;
    [Range(0f, 1f)] public float defaultMusicVolume = 0.6f;
    [Range(0f, 1f)] public float defaultSfxVolume = 0.8f;
    [Range(0.1f, 5f)] public float fadeDuration = 1.5f;

    private AudioSource musicSource;
    private AudioSource sfxSource;
    private Coroutine fadeCoroutine;

    void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Setup audio sources
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        sfxSource.loop = false;

        musicSource.volume = defaultMusicVolume;
        sfxSource.volume = defaultSfxVolume;

        // 🔁 Listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 🔄 Automatically switch music when scene changes
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name.ToLower();

        AudioClip nextClip = null;

        if (sceneName.Contains("mainmenu"))
            nextClip = musicLibrary.mainMenuMusic;
        else if (sceneName.Contains("main"))
            nextClip = musicLibrary.campusMusic;
        else if (sceneName.Contains("programming"))
            nextClip = musicLibrary.programmingLabMusic;
        // else if (sceneName.Contains("electrical"))
        //     nextClip = musicLibrary.electricalLabMusic;
        // else if (sceneName.Contains("chemistry"))
        //     nextClip = musicLibrary.chemistryLabMusic;
        // else if (sceneName.Contains("drawing"))
        //     nextClip = musicLibrary.drawingLabMusic;
        else if (sceneName.Contains("pause"))
            nextClip = musicLibrary.pauseMenuMusic;

        if (nextClip != null)
            PlayMusic(nextClip);
    }

    // 🎵 Universal music player
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        if (musicSource.clip == clip) return;

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeToNewClip(clip));
    }

    // 🏆 SFX
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    // 🌫 Fade transition
    private IEnumerator FadeToNewClip(AudioClip newClip)
    {
        float startVolume = musicSource.volume;

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0, defaultMusicVolume, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = defaultMusicVolume;
    }

    // 🎚 Volume control
    public void SetMusicVolume(float volume)
    {
        defaultMusicVolume = volume;
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        defaultSfxVolume = volume;
        sfxSource.volume = volume;
    }

    public void StopMusic() => musicSource.Stop();
}
