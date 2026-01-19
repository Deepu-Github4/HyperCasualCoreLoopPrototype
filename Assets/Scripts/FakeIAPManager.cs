using UnityEngine;

public class FakeIAPManager : MonoBehaviour
{
    // Simulates a successful IAP purchase
    public void BuyGemPack()
    {
        EconomyManager.Instance.AddGems(10);
        Debug.Log("Fake IAP: Purchased 10 Gems");
    }
}
