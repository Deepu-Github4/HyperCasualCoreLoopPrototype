using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PoolManager obstaclePool;

    [Header("Spawn Distance")]
    [SerializeField] private float spawnDistanceAhead = 20f;

    [Header("Obstacle Spacing")]
    [SerializeField] private float minGap = 6f;
    [SerializeField] private float maxGap = 10f;

    [Header("Coins")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private bool spawnCoins = true;
    [SerializeField] private int coinsPerLine = 3;
    [SerializeField] private float coinSpacing = 1.5f;
    [SerializeField] private float coinY = 1.2f;

    private float nextSpawnZ;

    void Start()
    {
        nextSpawnZ = player.position.z + spawnDistanceAhead;
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
            return;

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

    void SpawnObstacle()
    {
        GameObject obstacle = obstaclePool.Get();
        obstacle.transform.position = new Vector3(
            player.position.x,
            0.5f,
            nextSpawnZ
        );
    }

    void SpawnCoins()
    {
        for (int i = 0; i < coinsPerLine; i++)
        {
            GameObject coin = Instantiate(coinPrefab);
            coin.transform.position = new Vector3(
                player.position.x,
                coinY,
                nextSpawnZ + i * coinSpacing
            );
        }
    }
}
