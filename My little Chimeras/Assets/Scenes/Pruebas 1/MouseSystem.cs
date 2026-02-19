using UnityEngine;
using UnityEngine.UI;

public class MouseSystem : BaseSystem
{
    [Header("Crosshair")]
    public RectTransform crosshairUI;

    [Header("Rotation")]
    public float rotationSpeed = 15f;
    public float mouseDeadZone = 0.001f;

    [Header("Raycast")]
    public LayerMask aimLayerMask;

    private Camera cam;
    private Transform player;

    private Vector3 aimDirection;
    private bool hasValidAim;

    public Vector3 AimDirection => aimDirection;

    protected override void Initialize()
    {
        cam = Camera.main;
        player = transform;

        if (cam == null)
            Debug.LogError("MouseSystem: Main Camera not found.");

        if (crosshairUI == null)
            Debug.LogError("MouseSystem: Crosshair UI not assigned.");
    }

    void Update()
    {
        UpdateCrosshair();
        UpdateAimFromMouseDelta();
        RotatePlayer();
    }

    // -------------------------
    // UI CROSSHAIR
    // -------------------------
    void UpdateCrosshair()
    {
        if (crosshairUI == null) return;

        crosshairUI.position = Input.mousePosition;
    }

    // -------------------------
    // AIM UPDATE (ONLY ON MOUSE MOVE)
    // -------------------------
    void UpdateAimFromMouseDelta()
    {
        if (cam == null) return;

        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        // Mouse did not move → do nothing
        if (Mathf.Abs(mouseX) < mouseDeadZone &&
            Mathf.Abs(mouseY) < mouseDeadZone)
            return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, player.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            Vector3 dir = hitPoint - player.position;
            dir.y = 0f;

            if (dir.sqrMagnitude > 0.001f)
            {
                aimDirection = dir.normalized;
                hasValidAim = true;
            }
        }
    }

    // -------------------------
    // ROTATION
    // -------------------------
    void RotatePlayer()
    {
        if (!hasValidAim)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(aimDirection, Vector3.up);

        player.rotation = Quaternion.Slerp(
            player.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }
}
