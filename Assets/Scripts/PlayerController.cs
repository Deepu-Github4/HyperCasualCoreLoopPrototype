using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    [Header("Movement")]
    [SerializeField] float baseSpeed = 6f;
    [SerializeField] float maxSpeed = 12f;
    [SerializeField] float speedIncreaseRate = 0.02f;

    [Header("Jump")]
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float jumpDuration = 0.35f;
    [SerializeField] float jumpCooldown = 1.2f;
    [SerializeField] float jumpForwardMultiplier = 1.5f;
    [Header("Booster")]
    [SerializeField] float boosterMultiplier = 1.8f;
    [SerializeField] float boosterDuration = 5f;
    [Header("Booster UI")]
    [SerializeField] private GameObject boosterActiveText;


    bool boosterActive;
    float boosterTimer;


    bool isRunningInput;   // ONLY input
    bool canMove;          // ONLY game state
    bool isJumping;

    float currentSpeed;
    float baseY;
    float jumpTimer;
    float lastJumpTime;

    void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
        baseY = transform.position.y;
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        if (!canMove)
            return;

        UpdateBooster();
        IncreaseDifficulty();
        MoveForward();
        HandleJump();
    }

    public void ActivateBooster()
    {
        boosterActive = true;
        boosterTimer = boosterDuration;

        if (boosterActiveText != null)
            boosterActiveText.SetActive(true);
    }


    public bool IsBoosterActive()
    {
        return boosterActive;
    }

    void UpdateBooster()
    {
        if (!boosterActive) return;

        boosterTimer -= Time.deltaTime;

        if (boosterTimer <= 0f)
        {
            boosterActive = false;

            if (boosterActiveText != null)
                boosterActiveText.SetActive(false);
        }
    }



    // ---------- INPUT ----------
    public void RunButtonDown()
    {
        isRunningInput = true;
    }

    public void RunButtonUp()
    {
        isRunningInput = false;
    }

    public void JumpButtonPressed()
    {
        if (isJumping) return;
        if (Time.time - lastJumpTime < jumpCooldown) return;

        isJumping = true;
        jumpTimer = 0f;
        lastJumpTime = Time.time;

        AudioManager.Instance.PlayJump();
    }

    // ---------- MOVEMENT ----------
    void MoveForward()
    {
        if (!isRunningInput && !isJumping)
            return;

        float speed = currentSpeed;

        if (boosterActive)
            speed *= boosterMultiplier;

        if (isJumping)
            speed *= jumpForwardMultiplier;

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

   

    void HandleJump()
    {
        if (!isJumping) return;

        jumpTimer += Time.deltaTime;
        float t = jumpTimer / jumpDuration;

        if (t >= 1f)
        {
            isJumping = false;
            SetY(baseY);
            return;
        }

        float height = Mathf.Sin(t * Mathf.PI) * jumpHeight;
        SetY(baseY + height);
    }

    void SetY(float y)
    {
        Vector3 pos = transform.position;
        pos.y = y;
        transform.position = pos;
    }

    void IncreaseDifficulty()
    {
        currentSpeed = Mathf.Min(
            baseSpeed + transform.position.z * speedIncreaseRate,
            maxSpeed
        );
    }

    // ---------- GAME STATE ----------
    public void EnableMovement()
    {
        canMove = true;
        isRunningInput = false; // MUST re-touch
    }

    public void DisableMovement()
    {
        canMove = false;
        isRunningInput = false;
        boosterActive = false;

        if (boosterActiveText != null)
            boosterActiveText.SetActive(false);

    }

    public void OnRevive()
    {
        isJumping = false;
        jumpTimer = 0f;
        SetY(baseY);

        // Move player out of obstacle
        transform.position += Vector3.forward * 0.6f;

        EnableMovement(); // BUT still requires touch
        boosterActive = false;

        if (boosterActiveText != null)
            boosterActiveText.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canMove) return;

        if (other.CompareTag("Obstacle"))
        {
            // IGNORE collision if booster is active
            if (boosterActive)
                return;

            GameManager.Instance.GameOver(other);
        }
    }

}
