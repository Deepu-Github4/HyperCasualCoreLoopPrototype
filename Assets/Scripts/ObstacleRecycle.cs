using UnityEngine;

public class ObstacleRecycle : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (transform.position.z < player.position.z - 10f)
        {
            gameObject.SetActive(false);
        }
    }
}
