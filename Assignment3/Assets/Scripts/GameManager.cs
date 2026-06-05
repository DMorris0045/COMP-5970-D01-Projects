using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    float score;

    public AudioSource audioSource;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverText.gameObject.SetActive(true);
    }
    
    public void PlayExplosionSound()
    {
        audioSource.PlayOneShot(explosionSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void AddScore()
    {
        score += 1f;
        scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();
    }
}
