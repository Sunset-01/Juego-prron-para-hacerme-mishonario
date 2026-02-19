

using UnityEngine;
using UnityEngine.InputSystem;

public class CarryAndShootBallIso : MonoBehaviour
{
    public float holdDistance = 1.5f;
    public float shootForce = 12f;

    Rigidbody rb;
    PlayerMovementRB holder;
    bool isHeld = false;

    InputAction shootAction;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        shootAction = new InputAction("Shoot", InputActionType.Button);
        shootAction.AddBinding("<Keyboard>/space");
        shootAction.AddBinding("<Mouse>/leftButton");
    }

    void OnEnable()
    {
        shootAction.Enable();
    }

    void OnDisable()
    {
        shootAction.Disable();
    }

    void Update()
    {
        if (!isHeld || holder == null) return;

        FollowPlayer();

        if (shootAction.WasPressedThisFrame())
        {
            Shoot();
        }
    }


    void FollowPlayer()
    {
        Vector3 dir = holder.LastMoveDirection;
        Vector3 targetPos = holder.transform.position + dir * holdDistance;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * 15f
        );
    }


    void Shoot()
    {
        Vector3 dir = holder.LastMoveDirection;

        isHeld = false;
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;

        rb.AddForce(dir * shootForce, ForceMode.Impulse);
        holder = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isHeld) return;

        PlayerMovementRB player = collision.gameObject.GetComponent<PlayerMovementRB>();
        if (player == null) return;

        holder = player;
        isHeld = true;
        rb.isKinematic = true;
    }
}
