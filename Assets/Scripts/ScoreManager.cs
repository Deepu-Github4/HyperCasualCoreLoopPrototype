using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI scoreText;

    private float startZ;

    void Start()
    {
        startZ = player.position.z;
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
            return;

        int score = Mathf.Max(0, Mathf.FloorToInt(player.position.z - startZ));
        scoreText.text = score.ToString();
    }
}
