using UnityEngine;

public class TriggerCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {

            Debug.Log("Aya");
        }
    }
}
