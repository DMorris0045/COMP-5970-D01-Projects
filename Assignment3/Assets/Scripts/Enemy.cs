using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float waveAmount;
    public float waveSpeed;

    float startY;

    Vector3 moveDirection = Vector3.right;

    public GameObject enemyBulletPrefab;
    public Transform enemyFirePoint;
    public AudioSource audioSource;
    public AudioClip shootSound;

    float fireRate = 1.5f;
    float nextFireTime = 0f;

    void Start()
    {
        moveSpeed = Random.Range(1.5f, 3f);
        waveAmount = Random.Range(0.2f, 1.5f);
        waveSpeed = Random.Range(1f, 2f);

        startY = transform.position.y;

        nextFireTime = Time.time + Random.Range(0f, fireRate);
    }

    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        float y = startY + Mathf.Sin(Time.time * waveSpeed) * waveAmount;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);

        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }

        if (transform.position.x > 4.5f || transform.position.x < -4.5f)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    void Shoot()
    {
        audioSource.PlayOneShot(shootSound);
        Instantiate(enemyBulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
    }
}