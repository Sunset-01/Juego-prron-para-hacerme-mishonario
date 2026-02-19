////using UnityEngine;
////using UnityEngine.InputSystem;

////[RequireComponent(typeof(Rigidbody))]
////public class PlayerMovementRB : MonoBehaviour
////{
////    [Header("Movement")]
////    public float moveSpeed = 5f;

////    private Vector2 moveInput;
////    private Rigidbody rb;

////    void Awake()
////    {
////        rb = GetComponent<Rigidbody>();
////    }

////    void FixedUpdate()
////    {
////        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

////        Vector3 velocity = move * moveSpeed;
////        velocity.y = rb.linearVelocity.y; // conserva gravedad

////        rb.linearVelocity = velocity;
////    }

////    // M�todo llamado por el Input System
////    public void OnMove(InputAction.CallbackContext context)
////    {
////        moveInput = context.ReadValue<Vector2>();
////    }
////}

//using UnityEngine;
//using UnityEngine.InputSystem;

//[RequireComponent(typeof(Rigidbody))]
//public class PlayerMovementRB : MonoBehaviour
//{
//    [Header("Movement")]
//    public float moveSpeed = 5f;

//    private Vector2 moveInput;
//    private Rigidbody rb;

//    // 🔥 Dirección pública para otros sistemas (pelota, ataques, etc.)
//    public Vector3 LastMoveDirection { get; private set; } = Vector3.forward;

//    void Awake()
//    {
//        rb = GetComponent<Rigidbody>();
//    }

//    void FixedUpdate()
//    {
//        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

//        if (move.sqrMagnitude > 0.001f)
//        {
//            LastMoveDirection = move.normalized;
//        }

//        Vector3 velocity = move * moveSpeed;
//        velocity.y = rb.linearVelocity.y;

//        rb.linearVelocity = velocity;
//    }

//    // Input System
//    public void OnMove(InputAction.CallbackContext context)
//    {
//        moveInput = context.ReadValue<Vector2>();
//    }
//}

using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementRB : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    private Vector2 moveInput;
    private Rigidbody rb;

    // 🔥 Dirección usada para disparos, ataques, etc.
    public Vector3 LastMoveDirection { get; private set; } = Vector3.forward;

    // 🔥 Control de personaje activo
    public bool IsActive { get; private set; } = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!IsActive) return;

        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        if (move.sqrMagnitude > 0.001f)
        {
            LastMoveDirection = move.normalized;
        }

        Vector3 velocity = move * moveSpeed;
        velocity.y = rb.linearVelocity.y; // conserva gravedad

        rb.linearVelocity = velocity;
    }

    // Método llamado por el Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        if (!IsActive)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = context.ReadValue<Vector2>();
    }

    // 🔁 Activar / desactivar control desde el manager
    public void SetActive(bool active)
    {
        IsActive = active;

        if (!active)
        {
            moveInput = Vector2.zero;
            rb.linearVelocity = Vector3.zero;
        }
    }
}
