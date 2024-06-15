using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollision : MonoBehaviour
{
    public GameObject road;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            SpawnRoad(road);
        }
    }

    void SpawnRoad(GameObject road)
    {
        road.transform.position = new Vector3(1f, 1f, 1f);
    }
}
