using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    float score = 0f;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timerText;

    public float counter = 60.0f;

    void Start()
    {
        Time.timeScale = 1f;

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }

        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Round(counter);
        }
        else
        {
            Debug.LogError("Timer Text is not assigned in the Inspector!");
        }
    }

    void Update()
    {
        if (timerText == null)
        {
            return;
        }

        if (counter > 0)
        {
            counter -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(counter);
        }
        else
        {
            GameOver();
        }

        if (Time.timeScale == 0f && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0f;

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }

    public void ScoreUpdate()
    {
        score += 10f;
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}