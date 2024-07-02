using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject obstaclePrefab;
    public int initialRoadPoolSize = 10;
    public int maxRoadPoolSize = 20;
    public int initialObstaclePoolSize = 10;
    public int maxObstaclePoolSize = 20;

    private ObjectPool<GameObject> roadPool;
    private ObjectPool<GameObject> obstaclePool;

    void Awake()
    {
        roadPool = new ObjectPool<GameObject>(
            CreateRoad,
            OnTakeRoadFromPool,
            OnReturnRoadToPool,
            OnDestroyRoadObject,
            true,
            initialRoadPoolSize,
            maxRoadPoolSize
        );

        obstaclePool = new ObjectPool<GameObject>(
            CreateObstacle,
            OnTakeObstacleFromPool,
            OnReturnObstacleToPool,
            OnDestroyObstacleObject,
            true,
            initialObstaclePoolSize,
            maxObstaclePoolSize
        );

        PreWarmPool(roadPool, initialRoadPoolSize);
        PreWarmPool(obstaclePool, initialObstaclePoolSize);
    }

    void PreWarmPool(ObjectPool<GameObject> pool, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = pool.Get();
            pool.Release(obj);
        }
    }

    GameObject CreateRoad()
    {
        return Instantiate(roadPrefab);
    }

    GameObject CreateObstacle()
    {
        return Instantiate(obstaclePrefab);
    }

    void OnTakeRoadFromPool(GameObject obj)
    {
        obj.SetActive(true);
        // Additional setup logic for road if needed
    }

    void OnReturnRoadToPool(GameObject obj)
    {
        obj.SetActive(false);
        // Reset road object if needed
    }

    void OnDestroyRoadObject(GameObject obj)
    {
        Destroy(obj);
    }

    void OnTakeObstacleFromPool(GameObject obj)
    {
        obj.SetActive(true);
        // Additional setup logic for obstacle if needed
    }

    void OnReturnObstacleToPool(GameObject obj)
    {
        obj.SetActive(false);
        // Reset obstacle object if needed
    }

    void OnDestroyObstacleObject(GameObject obj)
    {
        Destroy(obj);
    }

    public GameObject GetRoad()
    {
        return roadPool.Get();
    }

    public void ReleaseRoad(GameObject obj)
    {
        roadPool.Release(obj);
    }

    public GameObject GetObstacle()
    {
        return obstaclePool.Get();
    }

    public void ReleaseObstacle(GameObject obj)
    {
        obstaclePool.Release(obj);
    }
}
