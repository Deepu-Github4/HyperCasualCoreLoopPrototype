using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        int multiplier = 1;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && player.IsBoosterActive())
            multiplier = 2;

        EconomyManager.Instance.AddCoins(1 * multiplier);
        AudioManager.Instance.PlayCoin();

        CoinPool.Instance.Return(gameObject);
    }
}
