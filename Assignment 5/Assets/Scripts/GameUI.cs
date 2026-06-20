using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    float score;
    float messageTimer = 0f;
    static int numLives = 3;

    public TextMeshProUGUI scoreText;
    public GameObject reversedNotif;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;

    void Start()
    {
        Time.timeScale = 1f;

        reversedNotif.SetActive(false);
        gameOverText.gameObject.SetActive(false);

        livesText.text = "Lives: " + numLives;
    }

    void Update()
    {
        score += Time.deltaTime;
        scoreText.text = "Score: " + Mathf.FloorToInt(score);

        if (Time.timeScale == 0f && Keyboard.current.spaceKey.isPressed)
        {
            RestartGame();
        }

        if (messageTimer > 0)
        {
            messageTimer -= Time.deltaTime;

            if (messageTimer <= 0)
            {
                reversedNotif.SetActive(false);
            }
        }
    }

    public void LoseLife()
    {
        numLives--;
        livesText.text = "Lives: " + numLives;

        if (numLives <= 0)
        {
            GameOver();
        }
    }

    public static bool HasLivesLeft()
    {
        return numLives > 0;
    }

    public void ShowReverseMessage(float duration)
    {
        reversedNotif.SetActive(true);
        messageTimer = duration;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverText.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        numLives = 3;
        SceneManager.LoadScene(1);
    }
}