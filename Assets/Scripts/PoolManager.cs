using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(obstaclePrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        pool.Enqueue(obj);
        return obj;
    }
}
