using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private bool isDead;

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Obstacle"))
        {
            isDead = true;

            // Stop movement immediately
            GetComponent<PlayerController>().enabled = false;

            GameManager.Instance.GameOver();
        }
    }
}
