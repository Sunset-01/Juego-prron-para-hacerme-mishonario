using UnityEngine;

public class SurfaceSystem : BaseSystem
{
    [Header("Ground Detection")]
    public float groundAngleLimit = 45f;

    [HideInInspector] public bool isGrounded;
    [HideInInspector] public Vector3 groundNormal = Vector3.up;

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = false;

        foreach (ContactPoint contact in collision.contacts)
        {
            float angle = Vector3.Angle(contact.normal, Vector3.up);

            if (angle <= groundAngleLimit)
            {
                isGrounded = true;
                groundNormal = contact.normal;
                return;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
        groundNormal = Vector3.up;
    }
}
