using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("UI")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI timerText;

    [Header("Win Result")]
    public TextMeshProUGUI coinResultText;
    public TextMeshProUGUI timeResultText;

    [Header("Tutorial")]
    public GameObject tutorialPanel;
    private bool gameStarted = false;

    [Header("Panels")]
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    [Header("Player")]
    public FirstPersonController playerController;

    [Header("Coin")]
    public int totalCoin = 5;
    private int currentCoin = 0;

    [Header("Timer")]
    public float startTime = 60f;
    private float currentTime;

    private bool gameEnded = false;
    private bool isPaused = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        currentTime = startTime;

        gameStarted = false;

        Time.timeScale = 0;

        tutorialPanel.SetActive(true);

        if (winPanel != null)
            winPanel.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        UpdateUI();

        playerController.UnlockCursor();
    }

    public void StartGame()
    {
        gameStarted = true;

        Time.timeScale = 1;

        tutorialPanel.SetActive(false);

        playerController.LockCursor();
    }

    private void Update()
    {
        if (gameEnded || isPaused || !gameStarted)
            return;

        currentTime -= Time.deltaTime;

        currentTime = Mathf.Clamp(currentTime, 0, startTime);

        if (timerText != null)
            timerText.text = Mathf.CeilToInt(currentTime).ToString();

        if (currentTime <= 0)
        {
            GameOver();
        }
    }

    public void AddCoin()
    {
        if (gameEnded)
            return;

        currentCoin++;

        UpdateUI();

        if (currentCoin >= totalCoin)
        {
            WinGame();
        }
    }

    private void UpdateUI()
    {
        if (coinText != null)
            coinText.text = currentCoin + " / " + totalCoin;
    }

    private void WinGame()
    {
        gameEnded = true;

        // Hitung waktu yang dipakai
        float usedTime = startTime - currentTime;

        int minutes = Mathf.FloorToInt(usedTime / 60);
        int seconds = Mathf.FloorToInt(usedTime % 60);

        // Tampilkan hasil pada panel win
        if (coinResultText != null)
            coinResultText.text = "Coin : " + currentCoin + " / " + totalCoin;

        if (timeResultText != null)
            timeResultText.text = "Time : " + minutes.ToString("00") + ":" + seconds.ToString("00");

        Time.timeScale = 0;

        if (playerController != null)
            playerController.UnlockCursor();

        if (winPanel != null)
            winPanel.SetActive(true);

        Debug.Log("YOU WIN");
    }

    private void GameOver()
    {
        gameEnded = true;

        Time.timeScale = 0;

        if (playerController != null)
            playerController.UnlockCursor();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log("GAME OVER");
    }

    // ========================
    // BUTTON FUNCTIONS
    // ========================

    public void PauseGame()
    {
        if (gameEnded || isPaused)
            return;

        isPaused = true;

        Time.timeScale = 0;

        if (playerController != null)
            playerController.UnlockCursor();

        if (pausePanel != null)
            pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        if (!isPaused)
            return;

        isPaused = false;

        Time.timeScale = 1;

        if (playerController != null)
            playerController.LockCursor();

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("MainMenu");
    }

    public void GoToLevelPanel()
    {
        Time.timeScale = 1;

        // Tandai bahwa MainMenu harus membuka LevelSelectPanel
        PlayerPrefs.SetInt("OpenLevelPanel", 1);

        SceneManager.LoadScene("MainMenu");
    }
}