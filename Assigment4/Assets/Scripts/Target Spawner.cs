using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
    public Transform player;
    public GameObject target;
    public WorldSpawner worldSpawner;
    public GameObject deliveryTarget;

    float minSpawnDistance = 10f;
    float collectDistance = 1f;
    bool hasTarget = false;


    public AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip deliverySound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deliveryTarget.SetActive(false);
        Invoke(nameof(SpawnTargetInitial), 0.1f);
    }

    void SpawnTargetInitial()
    {
        SpawnTarget(false);
    }

    void SpawnTarget(bool ignoreDistance)
    {
        List<Vector3> candidates = new List<Vector3>();

        foreach (var kvp in worldSpawner.GetActiveChunks())
        {
            GameObject chunkObject = kvp.Value;

            if (chunkObject == null)
            {
                continue;
            }

            if (!ignoreDistance && Vector3.Distance(player.position, chunkObject.transform.position) < minSpawnDistance)
            {
                continue;
            }

            Tilemap road = chunkObject.transform.Find("Road")?.GetComponent<Tilemap>();

            if (road == null)
            {
                continue;
            }

            foreach (Vector3Int cellPosition in road.cellBounds.allPositionsWithin)
            {
                if (road.HasTile(cellPosition))
                {
                    Vector3 worldPosition = road.GetCellCenterWorld(cellPosition);
                    candidates.Add(worldPosition);
                }
            }
        }

        if (candidates.Count == 0)
        {
            Debug.LogWarning("No valid target spawn position found!");
            return;
        }

        target.transform.position = candidates[Random.Range(0, candidates.Count)];
        target.SetActive(true);
    }

    void spawnDeliveryTarget(bool ignoreDistance)
    {
        List<Vector3> candidates = new List<Vector3>();

        foreach (var kvp in worldSpawner.GetActiveChunks())
        {
            GameObject chunkObject = kvp.Value;

            if (chunkObject == null)
            {
                continue;
            }

            if (!ignoreDistance && Vector3.Distance(player.position, chunkObject.transform.position) < minSpawnDistance)
            {
                continue;
            }

            Tilemap road = chunkObject.transform.Find("Road")?.GetComponent<Tilemap>();

            if (road == null)
            {
                continue;
            }

            foreach (Vector3Int cellPosition in road.cellBounds.allPositionsWithin)
            {
                if (road.HasTile(cellPosition))
                {
                    Vector3 worldPosition = road.GetCellCenterWorld(cellPosition);
                    candidates.Add(worldPosition);
                }
            }
        }

        if (candidates.Count == 0)
        {
            Debug.LogWarning("No valid delivery spawn position found!");
            return;
        }

        deliveryTarget.transform.position = candidates[Random.Range(0, candidates.Count)];
        deliveryTarget.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null || player == null || worldSpawner == null)
        {
            return;
        }

        if (!hasTarget)
        {
            float distance = Vector3.Distance(player.position, target.transform.position);

            if (distance < collectDistance)
            {
                hasTarget = true;
                target.SetActive(false);
                spawnDeliveryTarget(false);
                PlayPickupSound();
                return;
            }

            Vector2Int targetCoord = worldSpawner.WorldToGrid(target.transform.position);

            if (!worldSpawner.IsChunkActive(targetCoord))
            {
                SpawnTarget(false);
            }
        }
        else
        {
            float distanceToDeliveryTarget = Vector3.Distance(player.position, deliveryTarget.transform.position);

            if (distanceToDeliveryTarget < collectDistance)
            {
                hasTarget = false;
                deliveryTarget.SetActive(false);
                SpawnTarget(false);
                PlayDeliverySound();
                FindAnyObjectByType<GameUI>().ScoreUpdate();
                return;
            }

            Vector2Int deliveryCoord = worldSpawner.WorldToGrid(deliveryTarget.transform.position);

            if (!worldSpawner.IsChunkActive(deliveryCoord))
            {
                spawnDeliveryTarget(false);
            }
        }
        
    }

    void PlayPickupSound()
    {
        audioSource.PlayOneShot(pickupSound);
    }

    void PlayDeliverySound()
    {
        audioSource.PlayOneShot(deliverySound);
    }

    public bool HasTarget()
    {
        return hasTarget;
    }
}