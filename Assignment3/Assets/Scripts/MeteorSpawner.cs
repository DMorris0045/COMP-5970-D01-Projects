using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{

    public GameObject meteorPrefab;

    public float spawnRate = 5f;
    public float minY = -5f;
    public float maxY = -3f;

    float nextSpawnTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnMeteor();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnMeteor()
    {
        bool spawnFromLeft = Random.value > 0.5f;

        float x = spawnFromLeft ? -3.5f : 3.5f;
        float y = Random.Range(minY, maxY);

        GameObject meteor = Instantiate(meteorPrefab, new Vector3(x , y, 0f), Quaternion.identity);

        Meteor meteorScript = meteor.GetComponent<Meteor>();

        if (!spawnFromLeft)
        {
            meteorScript.moveSpeed *= -1f;
        }
    }
}
