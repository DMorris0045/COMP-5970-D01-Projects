using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Meteor : MonoBehaviour
{
    public float moveSpeed = 2;
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = Random.Range(1.5f, 3f);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        moveDirection = (player.transform.position - transform.position).normalized;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (transform.position.x > 10f || transform.position.x < -10f ||
            transform.position.y > 10f || transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
