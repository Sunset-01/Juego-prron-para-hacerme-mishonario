using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicMovementSystem : BaseSystem
{
    [Header("Movement Speed")]
    public float maxSpeed = 5f;
    public float slowMultiplier = 0.4f;

    [Header("Movement Acceleration")]
    public float acceleration = 12f;
    public float deceleration = 18f;

    [Header("Jump Forces")]
    public float jumpForce = 8f;
    public float doubleJumpForce = 7f;
    public float gravity = -25f;
    public float doubleTapTime = 0.25f;

    [Header("Jump Precision System")]
    public float preJumpDecelDuration = 0.2f;
    public float postLandingDecelDuration = 0.2f;
    [Range(0f, 1f)] public float decelStrength = 0.4f;
    [Range(0f, 1f)] public float midAirSlowStrength = 0.6f;
    public bool useLerpSmoothing = true;

    [Header("Isometric Settings")]
    public float isometricAngle = 45f;

    private CharacterController controller;
    private KeybindSystem input;

    private Vector3 horizontalVelocity;
    private Vector3 externalInfluence; // ← NEW
    private float verticalVelocity;

    private bool canDoubleJump;
    private float lastJumpTapTime;

    private float preJumpTimer;
    private float postLandingTimer;
    private bool pendingJump;
    private float pendingJumpForce;

    private bool wasGroundedLastFrame;

    // -------------------------
    // PUBLIC CONTRACT
    // -------------------------

    public Vector3 Velocity
    {
        get => horizontalVelocity;
        set => horizontalVelocity = value;
    }

    public float VerticalVelocity => verticalVelocity; // ← EXPOSED

    public void AddExternalInfluence(Vector3 influence) // ← NEW
    {
        externalInfluence += influence;
    }

    protected override void Initialize()
    {
        controller = GetComponent<CharacterController>();
        input = SystemHub.Get<KeybindSystem>();

        if (controller == null || input == null)
            Debug.LogError("BasicMovementSystem: Missing dependencies");
    }

    void Update()
    {
        HandleMovement();
        HandleJumpAndGravity();
        ApplyMovement();

        wasGroundedLastFrame = controller.isGrounded;
    }

    // -------------------------
    // HORIZONTAL MOVEMENT
    // -------------------------
    void HandleMovement()
    {
        float targetSpeed = input.slowHeld
            ? maxSpeed * slowMultiplier
            : maxSpeed;

        Vector3 inputDir = new Vector3(
            input.moveInput.x,
            0f,
            input.moveInput.z
        ).normalized;

        Quaternion isoRot = Quaternion.AngleAxis(isometricAngle, Vector3.up);
        Vector3 isoDirection = isoRot * inputDir;

        Vector3 targetVelocity = isoDirection * targetSpeed;

        float accelRate = inputDir.magnitude > 0.01f
            ? acceleration
            : deceleration;

        horizontalVelocity = Vector3.MoveTowards(
            horizontalVelocity,
            targetVelocity,
            accelRate * Time.deltaTime
        );

        ApplyPrecisionModifiers();
    }

    // -------------------------
    // JUMP & GRAVITY
    // -------------------------
    void HandleJumpAndGravity()
    {
        if (controller.isGrounded)
        {
            verticalVelocity = -2f;
            canDoubleJump = true;

            if (!wasGroundedLastFrame)
                postLandingTimer = postLandingDecelDuration;
        }

        if (input.jumpPressed)
        {
            float timeSinceLastTap = Time.time - lastJumpTapTime;
            lastJumpTapTime = Time.time;

            if (controller.isGrounded)
            {
                StartPreJump(jumpForce);
            }
            else if (canDoubleJump && timeSinceLastTap <= doubleTapTime)
            {
                canDoubleJump = false;
                StartPreJump(doubleJumpForce);
            }
        }

        if (preJumpTimer > 0f)
        {
            preJumpTimer -= Time.deltaTime;

            if (preJumpTimer <= 0f && pendingJump)
            {
                verticalVelocity = pendingJumpForce;
                pendingJump = false;
            }
        }

        verticalVelocity += gravity * Time.deltaTime;
    }

    void StartPreJump(float force)
    {
        preJumpTimer = preJumpDecelDuration;
        pendingJump = true;
        pendingJumpForce = force;
    }

    // -------------------------
    // PRECISION MODIFIERS
    // -------------------------
    void ApplyPrecisionModifiers()
    {
        float modifier = 1f;

        if (preJumpTimer > 0f)
            modifier *= decelStrength;

        if (postLandingTimer > 0f)
        {
            postLandingTimer -= Time.deltaTime;
            modifier *= decelStrength;
        }

        if (!controller.isGrounded && !pendingJump)
            modifier *= midAirSlowStrength;

        if (useLerpSmoothing)
        {
            horizontalVelocity = Vector3.Lerp(
                horizontalVelocity,
                horizontalVelocity * modifier,
                10f * Time.deltaTime
            );
        }
        else
        {
            horizontalVelocity *= modifier;
        }
    }

    // -------------------------
    // APPLY MOVEMENT
    // -------------------------
    void ApplyMovement()
    {
        Vector3 finalHorizontal = horizontalVelocity + externalInfluence;

        Vector3 move =
            finalHorizontal + Vector3.up * verticalVelocity;

        controller.Move(move * Time.deltaTime);

        externalInfluence = Vector3.zero; // ← RESET EACH FRAME
    }
}
