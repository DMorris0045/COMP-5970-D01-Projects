using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public TextMeshProUGUI WinText;
    public GameObject goal;

    float moveSpeed = 5f;
    Rigidbody2D rb;

    int gemNeeded = 2;
    int gemsCollected = 0;
    float jumpForce = 7f;
    int maxJumps = 2;
    int jumpsRemaining;

    SpriteRenderer spriteRenderer;

    bool hasWon = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpsRemaining = maxJumps;

        if (goal != null)
        {
            goal.SetActive(false);
        }

        WinText.gameObject.SetActive(false);
    }

    void Update()
    {
        float moveInput = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            moveInput = -1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            moveInput = 1f;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && jumpsRemaining > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpsRemaining--;
        }

        if (moveInput > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput < 0)
        {
            spriteRenderer.flipX = false;
        }

        // Restart only after winning
        if (hasWon && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartLevel();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpsRemaining = maxJumps;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Water"))
        {
            RestartLevel();
        }

        if (other.CompareTag("Goal"))
        {
            WinText.gameObject.SetActive(true);
            hasWon = true;
            Time.timeScale = 0f;
        }

        if (other.CompareTag("Enemy"))
        {
            RestartLevel();
        }
    }

    public void CollectGem()
    {
        gemsCollected++;

        if (gemsCollected >= gemNeeded && goal != null)
        {
            goal.SetActive(true);
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}