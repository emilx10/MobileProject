using UnityEngine;

public class TriggerCollision : MonoBehaviour
{
    ScoreManager scoreManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            scoreManager.GameOver();
            Debug.Log("Ouch");
        }
    }
}
