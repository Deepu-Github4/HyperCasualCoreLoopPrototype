using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float baseSpeed = 6f;
    [SerializeField] private float maxSpeed = 12f;
    [SerializeField] private float speedIncreaseRate = 0.02f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float jumpDuration = 0.35f;
    [SerializeField] private float jumpCooldown = 1.2f;
    [SerializeField] private float jumpForwardMultiplier = 1.5f;

    private bool isRunning;
    private bool isJumping;

    private float currentSpeed;
    private float jumpTimer;
    private float baseY;
    private float lastJumpTime;

    void Start()
    {
        baseY = transform.position.y;
        currentSpeed = baseSpeed;
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameRunning)
            return;

        IncreaseDifficulty();
        MoveForward();
        HandleJump();
    }

    // ---------- RUN ZONE ----------
    public void RunButtonDown() => isRunning = true;
    public void RunButtonUp() => isRunning = false;

    // ---------- JUMP BUTTON ----------
    public void JumpButtonPressed()
    {
        if (isJumping) return;
        if (Time.time - lastJumpTime < jumpCooldown) return;

        isJumping = true;
        jumpTimer = 0f;
        lastJumpTime = Time.time;
    }

    // ---------- MOVEMENT ----------
    void MoveForward()
    {
        if (!isRunning && !isJumping) return;

        float speed = currentSpeed;
        if (isJumping) speed *= jumpForwardMultiplier;

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

    // ---------- DIFFICULTY ----------
    void IncreaseDifficulty()
    {
        currentSpeed = Mathf.Min(
            baseSpeed + transform.position.z * speedIncreaseRate,
            maxSpeed
        );
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}
