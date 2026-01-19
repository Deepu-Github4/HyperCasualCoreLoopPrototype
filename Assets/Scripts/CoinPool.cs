using UnityEngine;
using System.Collections.Generic;

public class CoinPool : MonoBehaviour
{
    public static CoinPool Instance;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private int poolSize = 30;

    private Queue<GameObject> available = new Queue<GameObject>();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        for (int i = 0; i < poolSize; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform);
            coin.SetActive(false);
            available.Enqueue(coin);
        }
    }

    public GameObject Get()
    {
        if (available.Count == 0)
        {
            Debug.LogWarning("CoinPool exhausted!");
            return null;
        }

        GameObject coin = available.Dequeue();
        coin.SetActive(true);
        return coin;
    }

    public void Return(GameObject coin)
    {
        coin.SetActive(false);
        available.Enqueue(coin);
    }
}
