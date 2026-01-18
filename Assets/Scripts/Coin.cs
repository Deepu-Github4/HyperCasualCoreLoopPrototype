using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EconomyManager.Instance.AddCoins(1);
            Destroy(gameObject);
        }
    }
}
