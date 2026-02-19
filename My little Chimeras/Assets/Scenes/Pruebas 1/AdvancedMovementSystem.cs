using UnityEngine;

public class AdvancedMovementSystem : BaseSystem
{
    [Header("Dash / Dodge")]
    public float dashSpeed = 14f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 3f;
    public float doubleTapTime = 0.25f;

    [Header("Isometric Settings")]
    public float isometricAngle = 45f;

    private KeybindSystem input;
    private BasicMovementSystem movement;

    private float lastDashTime;
    private float dashTimer;
    private Vector3 dashDirection;

    private KeyCode lastTapKey;
    private float lastTapTime;
    private bool releasedSinceLastTap;

    protected override void Initialize()
    {
        input = SystemHub.Get<KeybindSystem>();
        movement = SystemHub.Get<BasicMovementSystem>();

        if (input == null || movement == null)
            Debug.LogError("AdvancedMovementSystem: Missing dependencies");
    }

    void Update()
    {
        HandleDashInput();
        HandleDashMovement();
    }

    // -------------------------
    // DASH INPUT
    // -------------------------
    void HandleDashInput()
    {
        if (Time.time < lastDashTime + dashCooldown)
            return;

        CheckKey(KeyCode.W);
        CheckKey(KeyCode.S);
        CheckKey(KeyCode.A);
        CheckKey(KeyCode.D);
    }

    void CheckKey(KeyCode key)
    {
        if (Input.GetKeyUp(key))
            releasedSinceLastTap = true;

        if (!Input.GetKeyDown(key))
            return;

        if (lastTapKey != key || Time.time - lastTapTime > doubleTapTime)
        {
            lastTapKey = key;
            lastTapTime = Time.time;
            releasedSinceLastTap = false;
            return;
        }

        if (!releasedSinceLastTap)
            return;

        StartDash(key);
    }

    void StartDash(KeyCode key)
    {
        lastDashTime = Time.time;
        dashTimer = dashDuration;
        releasedSinceLastTap = false;

        Vector3 dir = key switch
        {
            KeyCode.W => Vector3.forward,
            KeyCode.S => Vector3.back,
            KeyCode.A => Vector3.left,
            KeyCode.D => Vector3.right,
            _ => Vector3.zero
        };

        Quaternion isoRot = Quaternion.AngleAxis(isometricAngle, Vector3.up);
        dashDirection = isoRot * dir.normalized;
    }

    // -------------------------
    // DASH EXECUTION
    // -------------------------
    void HandleDashMovement()
    {
        if (dashTimer <= 0f)
            return;

        dashTimer -= Time.deltaTime;
        movement.Velocity = dashDirection * dashSpeed;
    }
}
