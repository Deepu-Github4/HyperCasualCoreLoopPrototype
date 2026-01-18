using UnityEngine;

public class GroundLooper : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float tileLength = 200f;
    [SerializeField] private int totalTiles = 3;

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
            return;

        if (transform.position.z < player.position.z - tileLength)
        {
            transform.position += Vector3.forward * tileLength * totalTiles;
        }
    }
}
