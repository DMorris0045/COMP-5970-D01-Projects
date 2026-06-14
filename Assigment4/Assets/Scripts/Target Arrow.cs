using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetArrow : MonoBehaviour
{
    public Transform player;
    public Transform target;

    public TargetSpawner targetSpawner;
    public Transform deliveryTarget;

    void Update()
    {
        if (player == null || targetSpawner == null)
        {
            return;
        }

        Transform currentTarget;

        if (targetSpawner.HasTarget())
        {
            currentTarget = deliveryTarget;
        }
        else
        {
            currentTarget = target;
        }

        if (currentTarget == null || !currentTarget.gameObject.activeInHierarchy)
        {
            return;
        }

        Vector3 direction = currentTarget.position - player.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}