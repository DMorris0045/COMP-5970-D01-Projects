using UnityEngine;
using System.Collections.Generic;

public class WorldSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject[] chunkPrefabs;
    public GameObject startingChunkPrefab;

    public float chunkSize = 33f;
    public int chunksAhead = 5;
    public int chunksBehind = 1;

    private Dictionary<int, GameObject> activeChunks = new Dictionary<int, GameObject>();

    private int lastPlayerChunkZ;

    void Start()
    {
        SpawnStartingChunk();

        lastPlayerChunkZ = WorldToChunkZ(player.position);
        RefreshChunks();
    }

    void Update()
    {
        int currentPlayerChunkZ = WorldToChunkZ(player.position);

        if (currentPlayerChunkZ != lastPlayerChunkZ)
        {
            lastPlayerChunkZ = currentPlayerChunkZ;
            RefreshChunks();
        }
    }

    void RefreshChunks()
    {
        int playerChunkZ = WorldToChunkZ(player.position);

        int minZ = playerChunkZ - chunksBehind;
        int maxZ = playerChunkZ + chunksAhead;

        List<int> chunksToRemove = new List<int>();

        foreach (var chunk in activeChunks)
        {

            if (chunk.Key == 0)
            {
                continue;

            }

            if (chunk.Key < minZ || chunk.Key > maxZ)
            {
                chunksToRemove.Add(chunk.Key);
            }
        }

        foreach (int z in chunksToRemove)
        {
            Destroy(activeChunks[z]);
            activeChunks.Remove(z);
        }

        for (int z = minZ; z <= maxZ; z++)
        {

            if (z < 0)
            {
                continue;
            }

            if (!activeChunks.ContainsKey(z))
            {
                PlaceChunk(z);
            }
        }
    }

    void SpawnStartingChunk()
    {
        if (!activeChunks.ContainsKey(0))
        {
            Vector3 spawnPosition = new Vector3(0f, 0f, 0f);

            GameObject chunk = Instantiate(startingChunkPrefab, spawnPosition, Quaternion.identity);

            activeChunks.Add(0, chunk);
        }
    }

    void PlaceChunk(int z)
    {
        int randomIndex = Random.Range(0, chunkPrefabs.Length);

        Vector3 spawnPosition = new Vector3(0f, 0f, z * chunkSize);

        GameObject chunk = Instantiate(
            chunkPrefabs[randomIndex],
            spawnPosition,
            Quaternion.identity
        );

        activeChunks.Add(z, chunk);
    }

    int WorldToChunkZ(Vector3 worldPos)
    {
        return Mathf.FloorToInt(worldPos.z / chunkSize);
    }
}