using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;

    [Header("UI")]
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI timerText;

    public GameObject winText;
    public GameObject gameOverText;

    [Header("Coin")]
    private int coinCount = 0;
    public int totalCoin = 5;

    [Header("Timer")]
    public float timeRemaining = 60f;

    private bool gameEnded = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateUI();

        winText.SetActive(false);
        gameOverText.SetActive(false);
    }

    private void Update()
    {
        if (gameEnded)
            return;

        HandleTimer();
    }

    void HandleTimer()
    {
        timeRemaining -= Time.deltaTime;

        timeRemaining = Mathf.Clamp(timeRemaining, 0, 999);

        timerText.text = "Time : " + Mathf.Ceil(timeRemaining);

        // GAME OVER
        if (timeRemaining <= 0)
        {
            GameOver();
        }
    }

    public void AddCoin()
    {
        if (gameEnded)
            return;

        coinCount++;

        UpdateUI();

        // YOU WIN
        if (coinCount >= totalCoin)
        {
            YouWin();
        }
    }

    void UpdateUI()
    {
        coinText.text = "Coin : " + coinCount + " / " + totalCoin;
    }

    void YouWin()
    {
        gameEnded = true;

        winText.SetActive(true);

        Debug.Log("YOU WIN");
    }

    void GameOver()
    {
        gameEnded = true;

        gameOverText.SetActive(true);

        Debug.Log("GAME OVER");
    }
}