using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float moveSpeed = 5f;
    float minX = -2.4f;
    float maxX = 2.4f;
    float minY = -4.5f;
    float maxY = 4.5f;
    int health = 3;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public float fireRate = 0.25f;

    private float nextFireTime = 0f;
    private Vector2 moveInput;

    public AudioSource audioSource;
    public AudioClip shootSound;

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnAttack(InputValue value)
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        audioSource.PlayOneShot(shootSound);
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }


    private void Update()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.position += movement * moveSpeed * Time.deltaTime;
        Vector3 pos = transform.position;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        transform.position = pos;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Meteor"))
        {
            FindAnyObjectByType<GameManager>().PlayExplosionSound();
            Destroy(gameObject);
            FindObjectOfType<GameManager>().GameOver();
        }

        if (other.CompareTag("EnemyBullet"))
        {
            health--;
            if (health == 2)
            {
                    heart3.SetActive(false);
            }
            else if (health == 1)
            {
                    heart2.SetActive(false);
            }
            else if (health == 0)
            {
                    FindAnyObjectByType<GameManager>().PlayExplosionSound();
                heart1.SetActive(false);
                    Destroy(gameObject);
                    FindObjectOfType<GameManager>().GameOver();
            }
        }
    }
}