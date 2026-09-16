using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public void RestartGame()
    {
        PlayerManager.gameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
