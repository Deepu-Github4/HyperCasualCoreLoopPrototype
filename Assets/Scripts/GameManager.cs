using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Menu,
    BoosterOffer,
    Playing,
    RevivePopup
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private GameObject menuRoot;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject boosterOfferPopup;
    [SerializeField] private int boosterCost = 5;
    [SerializeField] private GameObject storeSet;

    bool startWithBooster;

    public GameState State { get; private set; }
    public bool IsGameRunning => State == GameState.Playing;

    private Collider lastHitObstacle;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ShowMenu();
    }

    // ---------------- MENU ----------------
    void ShowMenu()
    {
        State = GameState.Menu;

        menuCanvas.SetActive(true);
        menuRoot.SetActive(true);
        gameplayCanvas.SetActive(false);

        ReviveManager.Instance.HideRevivePopup(); 
        player.DisableMovement();
    }


    public void PlayGame()
    {
        AudioManager.Instance.ButtonClick();
        // Check if player can afford booster
        if (EconomyManager.Instance.Gems >= boosterCost)
        {
            State = GameState.BoosterOffer;
            menuRoot.SetActive(false);
            boosterOfferPopup.SetActive(true);
        }
        else
        {
            StartGame(false);
        }
    }

    public void ConfirmBooster()
    {
        AudioManager.Instance.ButtonClick();
        if (EconomyManager.Instance.SpendGems(boosterCost))
        {
            startWithBooster = true;
        }

        boosterOfferPopup.SetActive(false);
        StartGame(startWithBooster);
    }

    public void DeclineBooster()
    {
        AudioManager.Instance.ButtonClick();
        boosterOfferPopup.SetActive(false);
        StartGame(false);
    }

    void StartGame(bool withBooster)
    {
        menuCanvas.SetActive(false);
        gameplayCanvas.SetActive(true);

        ReviveManager.Instance.ResetRun();

        State = GameState.Playing;
        player.EnableMovement();

        if (withBooster)
        {
            player.ActivateBooster();
        }

        startWithBooster = false;
    }

    // ---------------- GAME OVER ----------------
    public void GameOver(Collider obstacle)
    {
        if (State != GameState.Playing) return;

        lastHitObstacle = obstacle;

        State = GameState.RevivePopup;
        player.DisableMovement();

        menuCanvas.SetActive(true);
        menuRoot.SetActive(false);
        boosterOfferPopup.SetActive(false);
        gameplayCanvas.SetActive(false);
        AudioManager.Instance.PlayFail();

        bool popupShown = ReviveManager.Instance.TryShowRevivePopup(OnReviveConfirmed);

        if (!popupShown)
        {
            ReloadScene();
        }

    }


    // ---------------- REVIVE ----------------
    private void OnReviveConfirmed()
    {
        if (lastHitObstacle != null)
            lastHitObstacle.enabled = false;

        menuCanvas.SetActive(false);
        menuRoot.SetActive(false);
        gameplayCanvas.SetActive(true);

        State = GameState.Playing;

        player.OnRevive();
        player.EnableMovement();
    }


    public void ShowMenuFromRevive()
    {
        ShowMenu();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StorePressed()
    {
        AudioManager.Instance.ButtonClick();
        storeSet.SetActive(true);
    }

    public void BackPressed()
    {
        AudioManager.Instance.ButtonClick();
        storeSet.SetActive(false);
    }
}
