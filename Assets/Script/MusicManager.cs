using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    public AudioSource audioSource;

    [Header("Music")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;

    private bool isMuted;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        isMuted = PlayerPrefs.GetInt("MusicMute", 0) == 1;
        audioSource.mute = isMuted;
    }

    public void PlayMainMenuMusic()
    {
        PlayMusic(mainMenuMusic);
    }

    public void PlayGameplayMusic()
    {
        PlayMusic(gameplayMusic);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip)
            return;

        audioSource.clip = clip;
        audioSource.Play();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        audioSource.mute = isMuted;

        PlayerPrefs.SetInt("MusicMute", isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool IsMuted()
    {
        return isMuted;
    }
}