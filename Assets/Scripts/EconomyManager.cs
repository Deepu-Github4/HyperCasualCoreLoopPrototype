using UnityEngine;
using TMPro;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager Instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI gemText;

    public int Coins { get; private set; }
    public int Gems { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        Coins = PlayerPrefs.GetInt("Coins", 0);
        Gems = PlayerPrefs.GetInt("Gems", 10); // starter premium currency

        UpdateUI();
    }

    // ---------- SOFT CURRENCY ----------
    public void AddCoins(int amount)
    {
        Coins += amount;
        PlayerPrefs.SetInt("Coins", Coins);
        UpdateUI();
    }

    // ---------- PREMIUM CURRENCY ----------
    public void AddGems(int amount)
    {
        Gems += amount;
        PlayerPrefs.SetInt("Gems", Gems);
        UpdateUI();
    }

    public bool SpendGems(int amount)
    {
        if (Gems < amount)
            return false;

        Gems -= amount;
        PlayerPrefs.SetInt("Gems", Gems);
        UpdateUI();
        return true;
    }

    void UpdateUI()
    {
        coinText.text = Coins.ToString();
        gemText.text = Gems.ToString();
    }
}
