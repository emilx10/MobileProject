using UnityEngine;

public class ObstacleDeactivator : MonoBehaviour
{
    public ObjectPoolManager poolManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Road"))
        {
            poolManager.ReleaseRoad(other.gameObject);
            Debug.Log("Deactivated road");
        }
        else if (other.CompareTag("Obstacle"))
        {
            poolManager.ReleaseObstacle(other.gameObject);
            Debug.Log("Deactivated obstacle");
        }
    }
}
