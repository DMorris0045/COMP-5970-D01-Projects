using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameUI gameUI = FindObjectOfType<GameUI>();

            if (gameUI != null)
            {
                gameUI.LoseLife();

                if (GameUI.HasLivesLeft())
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }
}