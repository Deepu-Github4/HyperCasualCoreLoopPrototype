using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public static ObstacleSpawner Instance;

    [SerializeField] private Transform player;
    [SerializeField] private PoolManager obstaclePool;
    [SerializeField] private CoinPool coinPool;

    [Header("Spawn Distance")]
    [SerializeField] private float spawnDistanceAhead = 20f;

    [Header("Obstacle Spacing")]
    [SerializeField] private float minGap = 6f;
    [SerializeField] private float maxGap = 10f;

    [Header("Lane Setup")]
    [SerializeField] private float[] lanes = { -1.5f, 0f, 1.5f };

    [Header("Coins")]
    [SerializeField] private bool spawnCoins = true;
    [SerializeField] private int coinsPerLine = 3;
    [SerializeField] private float coinSpacing = 1.5f;
    [SerializeField] private float coinY = 1.2f;

    [Header("Despawn")]
    [SerializeField] private float despawnDistance = 10f;

    private float nextSpawnZ;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        nextSpawnZ = player.position.z + spawnDistanceAhead;
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
            return;

        // 🔴 CRITICAL: clean up old obstacles
        CleanupObstacles();

        if (player.position.z + spawnDistanceAhead >= nextSpawnZ)
        {
            SpawnObstacle();

            float difficulty = Mathf.Clamp(player.position.z / 200f, 0f, 1f);
            float coinChance = Mathf.Lerp(0.5f, 0.25f, difficulty);

            if (spawnCoins && Random.value < coinChance)
                SpawnCoins();

            float gap = Mathf.Lerp(maxGap, minGap, difficulty);
            nextSpawnZ += gap;
        }
    }

    // ---------------- SPAWN ----------------
    void SpawnObstacle()
    {
        GameObject obstacle = obstaclePool.Get();
        if (obstacle == null) return;

        // Lane variety
        float laneX = lanes[Random.Range(0, lanes.Length)];

        obstacle.transform.position = new Vector3(
            laneX,
            0.5f,
            nextSpawnZ
        );
    }

    void SpawnCoins()
    {
        float centerLaneX = lanes[1];

        for (int i = 0; i < coinsPerLine; i++)
        {
            GameObject coin = coinPool.Get();
            if (coin == null) return;

            coin.transform.position = new Vector3(
                centerLaneX,
                coinY,
                nextSpawnZ + i * coinSpacing
            );
        }
    }


    // ---------------- CLEANUP ----------------
    void CleanupObstacles()
    {
        List<GameObject> activeObstacles = obstaclePool.GetActiveObjects();

        for (int i = activeObstacles.Count - 1; i >= 0; i--)
        {
            GameObject obs = activeObstacles[i];

            if (obs.transform.position.z < player.position.z - despawnDistance)
            {
                obstaclePool.Return(obs);
            }
        }
    }

    public void Resume()
    {
        
    }
}
