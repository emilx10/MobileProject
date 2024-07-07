using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPoolManager poolManager;

    public float roadSpawnInterval = 2f;
    public float roadStartDelay = 2f;

    public float obstacleSpawnInterval = 3f;
    public float obstacleStartDelay = 3f;

    private float roadSpawnTimer;
    private float obstacleSpawnTimer;

    [SerializeField] private float roadSpawnZ = -26f;
    [SerializeField] private float obstacleSpawnZ = -10f;

    void Start()
    {
        roadSpawnTimer = roadStartDelay;
        obstacleSpawnTimer = obstacleStartDelay;
    }

    void Update()
    {
        roadSpawnTimer -= Time.deltaTime;
        obstacleSpawnTimer -= Time.deltaTime;

        if (roadSpawnTimer <= 0)
        {
            SpawnRoad();
            roadSpawnTimer = roadSpawnInterval;
        }

        if (obstacleSpawnTimer <= 0)
        {
            SpawnObstacle();
            obstacleSpawnTimer = obstacleSpawnInterval;
        }
    }

    void SpawnRoad()
    {
        GameObject road = poolManager.GetRoad();
        road.transform.position = CalculateRoadSpawnPosition();
        road.tag = "Road";
        road.SetActive(true);
    }

    void SpawnObstacle()
    {
        GameObject obstacle = poolManager.GetObstacle();
        obstacle.transform.position = CalculateObstacleSpawnPosition();
        obstacle.tag = "Obstacle";
        obstacle.SetActive(true);
    }

    Vector3 CalculateRoadSpawnPosition()
    {
        float spawnX = 0f;
        float spawnZ = transform.position.z + roadSpawnZ;

        return new Vector3(spawnX, 0f, spawnZ);
    }

    Vector3 CalculateObstacleSpawnPosition()
    {
        float spawnX = Random.Range(-2f, 2f);
        float spawnZ = transform.position.z + obstacleSpawnZ;

        return new Vector3(spawnX, 0.5f, spawnZ);
    }
}
