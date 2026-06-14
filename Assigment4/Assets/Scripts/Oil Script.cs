using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilScript : MonoBehaviour
{

    public float slowMultiplier = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CarController car = other.GetComponent<CarController>();

        if (car != null)
        {
            car.OilSpill(slowMultiplier);
        }
    }
}
