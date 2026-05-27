using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject fallingObjectPrefab;

    float spawnInterval = 1f;
    float timer = 0f;

    float cubeSizeMax = 4f;
    float cubeSizeMin = 0.5f;

    void SpawnFallingObject()
    {
        float xPos = Random.Range(-8f, 8f);
        float randomSize = Random.Range(cubeSizeMin, cubeSizeMax);

        Vector3 spawnPos = new Vector3(xPos, transform.position.y, 0f);

        GameObject newObject = Instantiate(fallingObjectPrefab, spawnPos, Quaternion.identity);

        newObject.transform.localScale = Vector3.one * randomSize;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnFallingObject();
            timer = 0f;
        }
    }
}