using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            GameObject obstacle = ObjectPool.instance.GetPooledObject();

            if(obstacle != null )
            {
                obstacle.SetActive(true);
            }
        }
    }
}
