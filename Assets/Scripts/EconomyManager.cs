using UnityEngine;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;

    [SerializeField] private TextMeshProUGUI coinText;

    private int coins;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        coins = PlayerPrefs.GetInt("Coins", 0);
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        PlayerPrefs.SetInt("Coins", coins);
        UpdateUI();
    }

    void UpdateUI()
    {
        coinText.text = coins.ToString();
    }
}
