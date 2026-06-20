using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseControlls : MonoBehaviour
{
    public float reverseDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.ActivateReverseControls(reverseDuration);
            }

            GameUI gameUI = FindObjectOfType<GameUI>();

            if (gameUI != null)
            {
                gameUI.ShowReverseMessage(reverseDuration);
            }

            Destroy(gameObject);
        }
    }
}