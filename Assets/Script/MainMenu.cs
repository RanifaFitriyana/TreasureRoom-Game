using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Exit Panel")]
    public GameObject exitPanel;

    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;

    [Header("Sound Button")]
    public GameObject muteImage;      
    public GameObject unmuteImage;    

    private void Start()
    {
        // Putar musik Main Menu
        if (MusicManager.Instance != null)
        {
            MusicManager.Instance.PlayMainMenuMusic();
            UpdateSoundIcon();
        }

        // Jika datang dari game setelah menang
        if (PlayerPrefs.GetInt("OpenLevelPanel", 0) == 1)
        {
            if (mainMenuPanel != null)
                mainMenuPanel.SetActive(false);

            if (levelSelectPanel != null)
                levelSelectPanel.SetActive(true);

            PlayerPrefs.SetInt("OpenLevelPanel", 0);
        }
        else
        {
            if (mainMenuPanel != null)
                mainMenuPanel.SetActive(true);

            if (levelSelectPanel != null)
                levelSelectPanel.SetActive(false);
        }

        if (exitPanel != null)
            exitPanel.SetActive(false);
    }

    // =====================
    // LEVEL SELECT
    // =====================

    public void OpenLevelSelect()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        levelSelectPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("Level2");
    }

    // =====================
    // SOUND
    // =====================

    public void ToggleSound()
    {
        if (MusicManager.Instance == null)
            return;

        MusicManager.Instance.ToggleMute();
        UpdateSoundIcon();
    }

    private void UpdateSoundIcon()
    {
        if (MusicManager.Instance == null)
            return;

        bool isMuted = MusicManager.Instance.IsMuted();

        // Musik menyala -> tampilkan ikon speaker
        muteImage.SetActive(!isMuted);

        // Musik mati -> tampilkan ikon speaker dicoret
        unmuteImage.SetActive(isMuted);
    }

    // =====================
    // EXIT GAME
    // =====================

    public void ShowExitConfirmation()
    {
        exitPanel.SetActive(true);
    }

    public void CancelExit()
    {
        exitPanel.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}