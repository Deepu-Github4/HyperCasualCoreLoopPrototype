using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject menuUI;

    public bool IsGameRunning { get; private set; }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        ShowMenu();
    }

    void ShowMenu()
    {
        IsGameRunning = false;
        menuUI.SetActive(true);
    }

    public void PlayGame()
    {
        menuUI.SetActive(false);
        IsGameRunning = true;
    }

    public void GameOver()
    {
        if (!IsGameRunning) return;

        IsGameRunning = false;
        Invoke(nameof(ReloadScene), 0.5f);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
