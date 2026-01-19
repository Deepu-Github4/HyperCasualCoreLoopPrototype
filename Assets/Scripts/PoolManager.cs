using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 15;

    private Queue<GameObject> available = new Queue<GameObject>();
    private List<GameObject> active = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            available.Enqueue(obj);
        }
    }

    public GameObject Get()
    {
        if (available.Count == 0)
        {
            Debug.LogWarning("Obstacle pool exhausted!");
            return null;
        }

        GameObject obj = available.Dequeue();
        obj.SetActive(true);
        active.Add(obj);
        return obj;
    }

    public void Return(GameObject obj)
    {
        if (!active.Contains(obj))
            return;

        obj.SetActive(false);
        active.Remove(obj);
        available.Enqueue(obj);
    }

    public List<GameObject> GetActiveObjects()
    {
        return active;
    }
}
