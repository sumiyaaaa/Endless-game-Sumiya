using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject StartingText;

    public static int numberOfCoins;
    public Text coinsText;

    void Start()
    {
        gameOver = false;
        isGameStarted = false;
        Time.timeScale = 1;
        numberOfCoins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && gameOverPanel != null)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if (coinsText != null)
            coinsText.text = "Coins: " + numberOfCoins;

        if (SwipeManager.tap && StartingText != null)
        {
            isGameStarted = true;
            Destroy(StartingText);
        }
    }
}