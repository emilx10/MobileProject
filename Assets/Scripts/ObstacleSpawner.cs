using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public ObjectPoolManager poolManager;
    public GameObject roadPrefab;
    public float spawnInterval = 2f; // Interval between each spawn
    public float startDelay = 2f; // Delay before first spawn

    private float spawnTimer;

    private float roadSpawn = 20f;
    private float obstacleSpawn = 20f;
    void Start()
    {
        spawnTimer = startDelay;
    }

    void Update()
    {
        // Count down the timer
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            // Spawn road
            SpawnRoad();

            // Spawn obstacle
            SpawnObstacle();

            // Reset timer
            spawnTimer = spawnInterval;
        }
    }

    void SpawnRoad()
    {
        GameObject road = poolManager.GetRoad();
        road.transform.position = CalculateRoadSpawnPosition();
        road.SetActive(true);
    }

    void SpawnObstacle()
    {
        GameObject obstacle = poolManager.GetObstacle();
        obstacle.transform.position = CalculateObstacleSpawnPosition();
        obstacle.SetActive(true);
    }

    Vector3 CalculateRoadSpawnPosition()
    {
        // Calculate where to spawn the road (example)
        float spawnX = 0f; // Road spawns in the center
        float spawnZ = transform.position.z - roadSpawn;
        roadSpawn += 20;

        return new Vector3(spawnX, 0f, spawnZ);
    }

    Vector3 CalculateObstacleSpawnPosition()
    {
        // Calculate where to spawn the obstacle (example)
        float spawnX = Random.Range(-2f, 2f); // Randomize X position within range
        float spawnZ = transform.position.z - obstacleSpawn; // Spawn ahead of the player
        obstacleSpawn += 30;

        return new Vector3(spawnX, 0.5f, spawnZ);
    }
}
