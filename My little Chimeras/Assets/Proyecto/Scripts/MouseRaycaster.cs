//using UnityEngine;

//public class MouseRaycaster : MonoBehaviour
//{
//    [Header("Raycast")]
//    public LayerMask groundLayer;
//    public LayerMask interactableLayer;
//    public float rayDistance = 100f;

//    Camera cam;

//    void Awake()
//    {
//        cam = Camera.main;
//    }

//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;

//            // 1️⃣ Interactuables primero
//            if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
//            {
//                HandleInteractable(hit);
//                return;
//            }

//            // 2️⃣ Suelo
//            if (Physics.Raycast(ray, out hit, rayDistance, groundLayer))
//            {
//                HandleGround(hit);
//            }
//            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red, 1f);

//        }
//    }

//    void HandleInteractable(RaycastHit hit)
//    {
//        Debug.Log("🧱 Objeto clickeado: " + hit.collider.name);

//        // Si tiene script interactuable
//        IClickable clickable = hit.collider.GetComponent<IClickable>();
//        if (clickable != null)
//        {
//            clickable.OnClick();
//        }
//    }

//    void HandleGround(RaycastHit hit)
//    {
//        Debug.Log("🟫 Suelo clickeado en: " + hit.point);

//        // Ejemplo: mover personaje
//        // player.MoveTo(hit.point);
//    }


//}

using UnityEngine;
using UnityEngine.InputSystem;

public class MouseRaycaster : MonoBehaviour
{
    [Header("Raycast")]
    public LayerMask groundLayer;
    public LayerMask interactableLayer;
    public float rayDistance = 100f;

    Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Mouse.current == null) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            // 1️⃣ Interactuables
            if (Physics.Raycast(ray, out hit, rayDistance, interactableLayer))
            {
                HandleInteractable(hit);
                return;
            }

            // 2️⃣ Suelo
            if (Physics.Raycast(ray, out hit, rayDistance, groundLayer))
            {
                HandleGround(hit);
            }
        }
    }

    void HandleInteractable(RaycastHit hit)
    {
        Debug.Log("🧱 Objeto clickeado: " + hit.collider.name);

        IClickable clickable = hit.collider.GetComponent<IClickable>();
        if (clickable != null)
        {
            clickable.OnClick();
        }
    }

    void HandleGround(RaycastHit hit)
    {
        Debug.Log("🟫 Suelo clickeado en: " + hit.point);
    }
}
