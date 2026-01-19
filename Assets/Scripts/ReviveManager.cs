using UnityEngine;
using TMPro;

public class ReviveManager : MonoBehaviour
{
    public static ReviveManager Instance;

    [SerializeField] private GameObject revivePopup;
    [SerializeField] private TextMeshProUGUI reviveCostText;

    private int reviveCount;
    private System.Action onReviveConfirmed;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ResetRun()
    {
        reviveCount = 0;
    }

    int GetReviveCost()
    {
        // 2, 4, 6, 8...
        return (reviveCount + 1) * 2;
    }

    public bool TryShowRevivePopup(System.Action reviveCallback)
    {
        int cost = GetReviveCost();

        // No popup if player can't afford
        if (EconomyManager.Instance.Gems < cost)
            return false;

        reviveCostText.text = $"Revive for {cost} Gems?";
        revivePopup.SetActive(true);
        onReviveConfirmed = reviveCallback;

        return true;
    }

    // YES button
    public void ConfirmRevive()
    {
        AudioManager.Instance.ButtonClick();
        int cost = GetReviveCost();

        if (EconomyManager.Instance.SpendGems(cost))
        {
            reviveCount++;
            revivePopup.SetActive(false);

            onReviveConfirmed?.Invoke();
            Debug.Log("REVIVE CONFIRMED - GAME RESUMED");
        }
        else
        {
            DeclineRevive();
        }
    }

    // NO button
    public void DeclineRevive()
    {
        AudioManager.Instance.ButtonClick();
        revivePopup.SetActive(false);
        GameManager.Instance.ReloadScene();
    }


    public void HideRevivePopup()
    {
        revivePopup.SetActive(false);
    }

}
