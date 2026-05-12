using UnityEngine;

public class EditorCameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float fastMoveMultiplier = 3f;
    public float zoomSpeed = 10f;
    public float rotationSpeed = 3f;

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom();
    }

    void HandleMovement()
    {
        float speed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            speed *= fastMoveMultiplier;

        float h = Input.GetAxis("Horizontal"); // A/D
        float v = Input.GetAxis("Vertical");   // W/S

        Vector3 move = transform.forward * v + transform.right * h;

        // Subir / bajar (Q / E)
        if (Input.GetKey(KeyCode.E))
            move += Vector3.up;
        if (Input.GetKey(KeyCode.Q))
            move += Vector3.down;

        transform.position += move * speed * Time.deltaTime;
    }

    void HandleRotation()
    {
        // Rotar con click derecho (RMB)
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * 100f * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * 100f * Time.deltaTime;

            transform.eulerAngles += new Vector3(-mouseY, mouseX, 0f);
            Vector3 angles = transform.eulerAngles;
            angles.z = 0;
            transform.eulerAngles = angles;
        }

        // Pan con click medio (MMB)
        if (Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X") * moveSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * moveSpeed * Time.deltaTime;

            Vector3 move = -transform.right * mouseX - transform.up * mouseY;
            transform.position += move;
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            transform.position += transform.forward * scroll * zoomSpeed;
        }
    }
}