using UnityEngine;

public class KeybindSystem : BaseSystem
{
    [Header("Movement Keys")]
    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    [Header("Action Keys")]
    public KeyCode jump = KeyCode.Space;
    public KeyCode slow = KeyCode.LeftShift;

    [HideInInspector] public Vector3 moveInput;
    [HideInInspector] public bool jumpPressed;
    [HideInInspector] public bool slowHeld;

    void Update()
    {
        moveInput = Vector3.zero;

        if (Input.GetKey(forward)) moveInput += Vector3.forward;
        if (Input.GetKey(backward)) moveInput += Vector3.back;
        if (Input.GetKey(left)) moveInput += Vector3.left;
        if (Input.GetKey(right)) moveInput += Vector3.right;

        moveInput = moveInput.normalized;

        jumpPressed = Input.GetKeyDown(jump);
        slowHeld = Input.GetKey(slow);
    }
}
